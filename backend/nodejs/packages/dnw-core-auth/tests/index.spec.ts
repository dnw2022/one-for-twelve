import { Auth } from "../src";

describe("validateJwt => failure flows", () => {
  it("Token missing", async () => {
    // Given
    const token: string | undefined = undefined;
    // When
    const actual = await Auth.validateJwt(token, "", "");
    // Then
    expect(actual).toEqual({
      statusCode: 401,
      body: "Bearer token missing from Authorization header",
    });
  });
  it("Token invalid", async () => {
    // Given
    const token = "invalid";
    // When
    const actual = await Auth.validateJwt(token, "", "");
    // Then
    expect(actual).toEqual({
      statusCode: 401,
      body: "Unable to decode token",
    });
  });
  it("Kid missing from token header", async () => {
    // Given
    const token =
      "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
    // When
    const actual = await Auth.validateJwt(token, "", "");
    // Then
    expect(actual).toEqual({
      statusCode: 401,
      body: "Kid undefined or Alg HS256 not found in token headers",
    });
  });
  it("Kid missing from token header", async () => {
    // Given
    const token =
      "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
    // When
    const actual = await Auth.validateJwt(token, "", "");
    // Then
    expect(actual).toEqual({
      statusCode: 401,
      body: "Kid undefined or Alg HS256 not found in token headers",
    });
  });
});
