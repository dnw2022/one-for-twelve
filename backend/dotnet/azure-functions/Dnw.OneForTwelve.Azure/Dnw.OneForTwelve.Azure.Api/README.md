# Intro

Microsoft Azure Functions implementation of the game api.

# Deployment

After initial setup (see below) you can create / update the Azure Function using the cli command:

```
dotnet lambda deploy-serverless --stack-name Dnw-OneForTwelve-Aws-Api --s3-bucket dnw-templates-2022
```

# Initial setup

General instructions on how to get started with Azure Functions (out-of-process) can be found here:  

https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-cli-csharp?tabs=azure-cli%2Cisolated-process
https://www.cazzulino.com/net6functions.html
https://andrewlock.net/exploring-dotnet-6-part-2-comparing-webapplicationbuilder-to-the-generic-host/

## Install Azure Functions Core Tools

See: 

https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cmacos%2Ccsharp%2Cportal%2Cbash#v2

```
brew tap azure/functions
brew install azure-functions-core-tools@4
# if upgrading on a machine that has 2.x or 3.x installed:
brew link --overwrite azure-functions-core-tools@4
```

## Install 

See: 

https://learn.microsoft.com/en-us/cli/azure/install-azure-cli

```
brew update && brew install azure-cli
```

## Create Azure Function

```
func init Dnw.OneForTwelve.Azure.Api --worker-runtime dotnet-isolated --target-framework net6.0
func new --name HttpExample --template "HTTP trigger" --authlevel "anonymous"
```