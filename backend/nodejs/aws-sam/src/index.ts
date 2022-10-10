import { Context, APIGatewayProxyResult, APIGatewayEvent } from "aws-lambda";
import middy from "@middy/core";

import { Auth } from "@dnw-core/auth";

import { GameFactory, GameCache } from "@dnw-one-for-twelve/game-core";

export const lambdaHandler = async (
  event: APIGatewayEvent,
  context: Context
): Promise<APIGatewayProxyResult> => {
  const { languageCode, strategy } = event.pathParameters!;
  const token = event.headers["authorization"]?.replace("Bearer ", "");

  console.log(token);

  const authResult = await Auth.validateJwt(
    token,
    "https://www.googleapis.com/service_accounts/v1/jwk/securetoken@system.gserviceaccount.com",
    "one-for-twelve-32778"
  );
  if (authResult !== undefined) {
    return authResult;
  }

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
