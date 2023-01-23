# Initial Setup

```script
brew install pnpm
npm install --location=global verdaccio
npm install -g typescript

brew install nvm
nvm install v18.13.0
```

# Running Locally

In one terminal window start verdaccio:

```script
cd ./backend/nodejs/verdaccio
./start.sh 
```
In another terminal window:

```script
pnpm update
pnpm i

pnpm game-core-deploy
```

To deploy to AWS lambda:

```script
pnpm sam-deploy
```
