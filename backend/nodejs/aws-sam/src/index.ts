import { Context, APIGatewayProxyResult, APIGatewayEvent } from "aws-lambda";
import middy from "@middy/core";

import { GameFactory, GameCache } from "@dnw-one-for-twelve/game-core";

export const lambdaHandler = async (
  event: APIGatewayEvent,
  context: Context
): Promise<APIGatewayProxyResult> => {
  const { languageCode, strategy } = event.pathParameters!;
  const token = event.headers["Authorization"]?.replace("Bearer ", "");

  console.log(token);

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
