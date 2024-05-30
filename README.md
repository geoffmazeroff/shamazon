# shamazon
The project is a basic website that lists products for sale based on data from a 3rd-party API. The purpose is to help me learn about Razor pages with ASP.NET Core.

- [Project plan](ProjectPlan.md)
- [CHANGELOG](CHANGELOG.md)

## Running the project
You will need the .NET 8 SDK installed on your machine. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/8.0).

1. Clone the repository.
2. From the `./Shamazon/` directory, run `dotnet run`. The site will be available at `https://localhost:5073/`.

To change between Production and Development environments, set the `ASPNETCORE_ENVIRONMENT` environment variable to `Production` or `Development` in `launchSettings.json`. From that file you can also change the port number.

## Running tests

From the `./Shamazon.Tests/` directory, run `dotnet test`.