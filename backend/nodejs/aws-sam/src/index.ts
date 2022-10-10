import { Context, APIGatewayProxyResult, APIGatewayEvent } from "aws-lambda";
import middy from "@middy/core";
import admin from "firebase-admin";

import { GameFactory, GameCache } from "@dnw-one-for-twelve/game-core";

admin.initializeApp({ projectId: "one-for-twelve-32778" });

export const lambdaHandler = async (
  event: APIGatewayEvent,
  context: Context
): Promise<APIGatewayProxyResult> => {
  const { languageCode, strategy } = event.pathParameters!;
  const token = event.headers["authorization"]?.replace("Bearer ", "");

  console.log(JSON.stringify(event.headers));
  console.log(token);

  if (!token) {
    return {
      statusCode: 401,
      body: "Bearer token missing from Authorization header",
    };
  }

  const decodedToken = await admin.auth().verifyIdToken(token);

  console.log(decodedToken);

  await initCache();

  const game = await GameFactory.getGame(languageCode!, strategy!);

  return {
    statusCode: 200,
    body: JSON.stringify(game),
  };
};

const initCache = async () => {
  await GameCache.init();
};

await initCache();

export const handler = middy(lambdaHandler);
