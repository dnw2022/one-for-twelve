import * as functions from "firebase-functions";
import * as express from "express";

import { GameCache } from "./game_cache";
import { GameFactory } from "./game_factory";
import { DemoGameFactoryEn } from "./demo_game_factory_en";
import { QuestionSelectionStrategies } from "./models";
import { DemoGameFactoryNl } from "./demo_game_factory_nl";

const app = express();

app.get("/", (req, res) => {
  res.status(200).send("Hello anonymous");
});

app.get("/games/:languageCode/:strategy", async (req, res) => {
  let { languageCode, strategy } = req.params;

  await initCache(false);

  let game = null;

  if (languageCode === "English") {
    game = DemoGameFactoryEn.getDemo1();
  }

  if (strategy === QuestionSelectionStrategies.Demo.toString()) {
    game = DemoGameFactoryNl.getDemo1();
  } else {
    game = await GameFactory.getRandom(strategy);
  }

  res.status(200).send(game);
});

const initCache = async (useUnrevised: Boolean) => {
  const resourcesPath = "../resources";
  const wordFiles = [`${resourcesPath}/words.csv`];
  const questionFiles = [`${resourcesPath}/questions.csv`];

  if (useUnrevised) {
    wordFiles.push(`${resourcesPath}/words_unrevised.csv`);
    questionFiles.push(`${resourcesPath}/questions_unrevised.csv`);
  }

  await GameCache.Init(wordFiles, questionFiles);
};

exports.gameApi = functions.https.onRequest(app);
