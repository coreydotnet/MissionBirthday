# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test

## Frontend Angular
Prereqs: install NodeJS and NPM

1. Open `MissionBirthday.UI` directory in PowerShell or a command line
2. Run `npm install`
3. Run `ng build hound-bot`
4. Run `ng build` or `ng build --watch`

## Visual Studio

1. Run MissionBirth.Api

## User Secrets
Note: API keys and connection string credentials should not be checked into source control.

For local development, endpoints and API keys are needed for Computer Vision and Language Service. Endpoints are found in `appsettings.json`.

1. Right-click `MissionBirthday.Api` and choose Manage User Secrets.
2. Replace the contents of `secrets.json` with the following:
```
{
  "OcrOptions:DocReaderKey": "<Computer Vision key>",
  "LanguageServiceOptions:MbLanguageKey": "<Language key>",
  "HoundBotOptions:DirectLineSecret": "<Direct Line Secret>"
}
```
3. Replace `<...>` with a key from an Azure Computer Vision resource (ex. `MissionBirthdayRG` > `mb-docReader` > `Keys and Endpoint`).
4. Replace `<...` with a key from an Azure Language resource (ex. `MissionBirthdayRG` > `mb-language` > `Keys and Endpoint`).
5. Replace `<...>` with a secret from an Azure Bot resource (ex. `mb-hBot` > `Channels` > `Direct Line` > `mb-website` > `Secret Keys`).
5. Save and close `secrets.json`.

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)