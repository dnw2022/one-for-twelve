# Intro

Aws sam nodejs implementation of the game api written in typescript.

# Jwt bearer validation

The oidc configuration of the firebase oauth server for this project can be found here:

```
https://securetoken.google.com/one-for-twelve-32778/.well-known/openid-configuration
```

The firebase-admin npm package can be used to verify the token easily, but it has a lot of dependencies which means the zip file that is uploaded increases in size. It is also possible to verify it using the jsonwebtoken package:

https://www.npmjs.com/package/jsonwebtoken

That is what is done here in a separate package: ./packages/dnw-core-auth.
