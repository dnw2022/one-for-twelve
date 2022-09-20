# Test locally

To test locally with the emulators (functions and firestore):

firebase emulators:start (in one terminal window)
npm run build:watch (in a second terminal window)

npm start should do the same, but have to check further (maybe this will help: https://stackoverflow.com/questions/53280743/serving-firebase-functions-doesnt-hot-reload-on-windows)

# Sending http requests to local functions

The thunder collection is part of the project. You just need to install the Thunder Client vscode extension and the collection should appear under the collections tab.

Standard location of saved collections / requests is: ~/Library/Application\ Support/Code/User/globalStorage/rangav.vscode-thunder-client/ (see: https://github.com/rangav/thunder-client-support/issues/547). But in this case the collection is in the git repo. To show it you have to update the Thunder Client extension settings and mark the checkbox under Thunder Client - Save to Workspace.

# Using express instead of firebase https functions

https://blog.logrocket.com/rest-api-firebase-cloud-functions-typescript-firestore/

# Speeding up tsc and copy ./resources

https://stackoverflow.com/questions/38276862/is-there-a-way-to-use-npm-scripts-to-run-tsc-watch-nodemon-watch
