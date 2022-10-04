const path = require("path");
const TsconfigPathsPlugin = require("tsconfig-paths-webpack-plugin");
const nodeExternals = require("webpack-node-externals");
// To improve build times for large projects enable fork-ts-checker-webpack-plugin
// const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');

module.exports = {
  mode: "production",
  entry: "./src/index.ts",
  output: {
    filename: "index.js", // <-- Important
    libraryTarget: "this", // <-- Important
  },
  watch: false,
  context: __dirname, // to automatically find tsconfig.json
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        exclude: /node_modules/,
        use: {
          loader: "ts-loader",
          options: {
            transpileOnly: false, // Set to true if you are using fork-ts-checker-webpack-plugin
            projectReferences: true,
          },
        },
      },
    ],
  },
  resolve: {
    modules: ["node_modules", path.resolve(__dirname)],
    // TsconfigPathsPlugin will automatically add this
    // alias: {
    //   packages: path.resolve(__dirname, 'packages/'),
    // },
    extensions: [".js", ".ts"],
    plugins: [new TsconfigPathsPlugin({})],
  },
  optimization: {
    minimize: false,
  },
  target: "node", // important in order not to bundle built-in modules like path, fs, etc.
  externals: [
    nodeExternals({
      // all dependencies in the package.json file (both dev and not dev) will be excluded
      // from the bundle.
      // this WILL include `@myscope/*`
      allowlist: [/^@dnw-one-for-twelve\//],
    }),
  ],
};
