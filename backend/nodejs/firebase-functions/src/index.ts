import express from "express";
import * as admin from "firebase-admin";

import { GameFactory, GameCache } from "@dnw-one-for-twelve/game-core";

admin.initializeApp({ projectId: "one-for-twelve-32778" });

const app = express();

app.get("/", (req: any, res: any) => {
  res.status(200).send("Hello anonymous");
});

app.get("/games/:languageCode/:strategy", async (req: any, res: any) => {
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

    const game = await GameFactory.getGame(languageCode, strategy);

    return res.status(200).send(game);
  } catch (ex) {
    return res
      .status(401)
      .send(`Bearer token validation failed. Exception: ${ex}`);
  }
});

const initCache = async (useUnrevised: Boolean) => {
  const resourcesPath = __dirname;
  const wordFiles = [`${resourcesPath}/words.csv`];
  const questionFiles = [`${resourcesPath}/questions.csv`];

  if (useUnrevised) {
    wordFiles.push(`${resourcesPath}/words_unrevised.csv`);
    questionFiles.push(`${resourcesPath}/questions_unrevised.csv`);
  }

  await GameCache.init(wordFiles, questionFiles);
};

//tslint:disable-next-line no-floating-promises
initCache(false);

exports.gameApi = app;
