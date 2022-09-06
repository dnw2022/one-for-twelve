import * as functions from "firebase-functions";

import { GameFactory } from "./game_factory";
import { DemoGameFactoryNl } from "./demo_game_factory_nl";
import { DemoGameFactoryEn } from "./demo_game_factory_en";
import { GameCache } from "./game_cache";
import { QuestionSelector, QuestionCategories, QuestionLevels } from "./models";

const builder = functions.region("europe-west3");

// // Create and Deploy Your First Cloud Functions
// // https://firebase.google.com/docs/functions/write-firebase-functions

exports.sayHello = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    functions.logger.info(`Auth token: ${context.auth?.token.sub}`, {
      structuredData: false,
    });

    return `Hello ${data?.name ?? "anonymous"}!`;
  });
});

exports.getStats = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    await initCache(false);

    const stats = GameCache.getStats();

    return stats;
  });
});

exports.getRandomQuestion = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    const firstLetterAnswer = data?.firstLetterAnswer as String;
    const level = data?.level as Number;
    const category = data?.category as Number;

    await initCache(false);

    const question = GameFactory.getRandomQuestion(
      new QuestionSelector(
        firstLetterAnswer,
        level as QuestionLevels,
        category as QuestionCategories
      )
    );

    return question;
  });
});

exports.getPossibleQuestions = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    const firstLetterAnswer = data?.firstLetterAnswer as String;
    const level = data?.level as Number;
    const category = data?.category as Number;

    await initCache(false);

    const questions = GameFactory.getPossibleQuestions(
      new QuestionSelector(
        firstLetterAnswer,
        level as QuestionLevels,
        category as QuestionCategories
      )
    );

    return questions;
  });
});

exports.getRandomGame = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    const questionSelectionStrategy = data?.questionSelectionStrategy as String;
    const languageCode = (data?.languageCode as String) ?? "nl";

    await initCache(false);

    const game =
      languageCode === "nl"
        ? await GameFactory.getRandom(questionSelectionStrategy)
        : DemoGameFactoryEn.getDemo1();

    return game;
  });
});

exports.getRandomGameWithUnrevisedQuestions = builder.https.onCall(
  async (data, context) => {
    return call(context, async () => {
      const questionSelectionStrategy =
        data?.questionSelectionStrategy as String;
      const languageCode = (data?.languageCode as String) ?? "nl";

      await initCache(true);

      const game =
        languageCode === "nl"
          ? await GameFactory.getRandom(questionSelectionStrategy)
          : DemoGameFactoryEn.getDemo1();

      return game;
    });
  }
);

exports.getDemoGameNl1 = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    return DemoGameFactoryNl.getDemo1();
  });
});

exports.getDemoGameEn1 = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    return DemoGameFactoryEn.getDemo1();
  });
});

exports.getDemoGameEn1 = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    return DemoGameFactoryEn.getDemo1();
  });
});

exports.getDemoGameEnYenni1 = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    return DemoGameFactoryEn.getDemoYenni1();
  });
});

exports.getDemoGameEnYenni2 = builder.https.onCall(async (data, context) => {
  return call(context, async () => {
    return DemoGameFactoryEn.getDemoYenni2();
  });
});

const call = async (
  context: functions.https.CallableContext,
  doWork: () => Promise<any>
): Promise<any> => {
  functions.logger.info(
    `Running in emulator: ${process.env.FUNCTIONS_EMULATOR}`
  );

  if (!process.env.FUNCTIONS_EMULATOR) {
    if (context.auth === undefined) {
      return getUnauthenticatedMessage();
    }
  }

  return doWork();
};

const initCache = async (useUnrevised: Boolean) => {
  const wordFiles = ["./words.csv"];
  const questionFiles = ["./questions.csv"];

  if (useUnrevised) {
    wordFiles.push("./words_unrevised.csv");
    questionFiles.push("./questions_unrevised.csv");
  }

  await GameCache.Init(wordFiles, questionFiles);
};

const getUnauthenticatedMessage = () => {
  return {
    error: {
      code: 401,
      message: "Request had invalid credentials.",
      status: "UNAUTHENTICATED",
    },
  };
};
