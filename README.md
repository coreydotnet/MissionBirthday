# Introduction 
Mission Birthday is an easy-to-use food and resource finder for pregnant women and families. The goal is to ensure available food and supplies are provided for children at birth and every year after, regardless of individual circumstances.

Mission Birthday has several parts that work together to create an effective resource finder. Organizations and groups with free food and supplies need to be able to easily add their information. The people looking for food and supplies need to be able to quickly search and find resources near them. Mission Birthday features include:

- **Resource Search**: Zip code search for nearby food banks and resources
- **Hound Bot**: A chat bot that prompts the user for their location and performs a search for nearby resources
- **Provider Resource Submit**: A way for groups and organizations with free food or supplies to add their information to the database
- **Autofill Resource Information**: Based on a flyer or document, autofill the Mission Birthday resource form

## Technologies Used

- Azure Computer Vision – Optical Character Recognition
- Azure Language Service – Named Entity Recognition
- Azure Bot
- Bot Framework Composer
- Angular
- Ionic
- Capacitor
- ASP.NET Core Web API 
- Entity Framework Core
- SQL Database

## Try It Out

Visit [missionbirthday.azurewebsites.net](https://missionbirthday.azurewebsites.net/) to try out the project.

# Getting Started

## Software dependencies
- Visual Studio 2019
- .NET 5
- SQL Server, SQL Server localdb, or SQL Server Express
- Angular 13
- NodeJS
- NPM
- Bot Framework Composer (for Hound Bot)

# Build and Test

## Build Angular Frontend
Prereqs: install NodeJS and NPM

1. Open `MissionBirthday.UI` directory in PowerShell or a command line
2. Run `npm install`
3. Run `ng build hound-bot`
4. Run `ng build` or `ng build --watch`

This builds to an App directory that'll be served by MissionBirthday.Api (see below).

## Build Backend and Run Full Site

1. Open `MissionBirthday.sln` in Visual Studio
2. Setup user secrets (see below)
3. Build the solution
4. Run `MissionBirth.Api`

For development this will launch the Swagger page (`https://localhost:44352/swagger`). Navigate to `https://localhost:44352/` to view the Angular site.

## Hound Bot Solution

Located at: `~/mbHoundBot/mbHoundBot.sln`
Can be opened with Bot Framework Composer or Visual Studio.

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
