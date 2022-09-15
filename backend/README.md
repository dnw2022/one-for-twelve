# Game backend

The backend is a simple api that allows the flutter app to start new demo- or random games. The question / word bank consists of a couple of cvs files at the moment.

3 implementations of the API have been written:

1. google firebase functions (nodejs written in typescript)
2. amazon aws lambdas (.net 6/7 custom aws runtime written in c#)
3. microsoft azure functions (.net 6/7 written in c#)

Note that the csv files are in the /resources folder, so they can be shared.

Most of the .net code in 2) and 3) can also be reused and is therefore in the /dotnet/shared folder.

# Authentication

Firebase authentication is used by the flutter app. The app receives a bearer token that is used to make api calls. Every backend has to validate the bearer token in the auth header and return http status code 401 Unauthorized if the token is invalid or missing.

Its best to also the rest api approach when using google cloud functions as opposed to using HttpsCallable. When using HttpsCallable in the cloud_firestore dart package grpc is used and the token is sent. This approach has multiple drawbacks:

1. When using a simple (rest) api the flutter app can be configured to use a different backend api by just changing the base url. If for google cloud functions HttpsCallable is used we need some conditional logic in the flutter app.
2. the cloud_firestore dart package uses grpc in the background. On ios about 500k lines of c++ code need to be compiled which slows down the ios builds considerably.
