import * as functions from "firebase-functions";
import * as express from "express";
import * as admin from "firebase-admin";

import { GameCache } from "./game_cache";
import { GameFactory } from "./game_factory";
import { DemoGameFactoryEn } from "./demo_game_factory_en";
import { QuestionSelectionStrategies } from "./models";
import { DemoGameFactoryNl } from "./demo_game_factory_nl";

admin.initializeApp();

const app = express();

app.get("/", (req, res) => {
  res.status(200).send("Hello anonymous");
});

app.get("/games/:languageCode/:strategy", async (req, res) => {
  const { languageCode, strategy } = req.params;
  const token = req.header("Authorization")?.replace("Bearer ", "");

  console.log(token);

  if (!token) {
    return res
      .status(401)
      .send("Bearer token missing from Authorization header");
  }

  try {
    const decodedToken = await admin.auth().verifyIdToken(token);

    console.log(decodedToken);

    await initCache(false);

    let game = null;

    if (languageCode === "English") {
      game = DemoGameFactoryEn.getDemo1();
    } else if (
      strategy === QuestionSelectionStrategies[QuestionSelectionStrategies.Demo]
    ) {
      game = DemoGameFactoryNl.getDemo1();
    } else {
      game = await GameFactory.getRandom(strategy);
    }

    return res.status(200).send(game);
  } catch (ex) {
    return res
      .status(401)
      .send(`Bearer token validation failed. Exception: ${ex}`);
  }
});

const initCache = async (useUnrevised: Boolean) => {
  const resourcesPath = "./lib";
  const wordFiles = [`${resourcesPath}/words.csv`];
  const questionFiles = [`${resourcesPath}/questions.csv`];

  if (useUnrevised) {
    wordFiles.push(`${resourcesPath}/words_unrevised.csv`);
    questionFiles.push(`${resourcesPath}/questions_unrevised.csv`);
  }

  await GameCache.Init(wordFiles, questionFiles);
};

exports.gameApi = functions.https.onRequest(app);
