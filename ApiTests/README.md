# ApiTests (API tests)

NUnit test project for API testing using [RestSharp](https://restsharp.dev/). Includes a base test class and a sample client for [JSONPlaceholder](https://jsonplaceholder.typicode.com/).

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Network access (tests call public APIs)

## Setup

1. Restore and build the project:

   ```bash
   dotnet restore ApiTests/ApiTests.csproj
   dotnet build ApiTests/ApiTests.csproj
   ```

## Running tests locally

From the repository root:

```bash
dotnet test ApiTests/ApiTests.csproj
```

Options:

```bash
# Verbose output
dotnet test ApiTests --logger "console;verbosity=detailed"

# Run a single test
dotnet test ApiTests --filter "FullyQualifiedName~GetPosts"
```

## Running with Docker

Tests run during the image build. From the repository root:

```bash
docker build -f ApiTests/Dockerfile -t api-tests .
```

Or with Compose:

```bash
docker compose build api-tests
```

The container build restores, builds, and runs the test suite. No extra services are required for the sample tests (they use the public JSONPlaceholder API).

## Allure reports

After a test run, results are written to `allure-results/`. Generate and open the report with:

```bash
allure serve allure-results
```

## Project structure

```
ApiTests/
├── BaseApiTest.cs               # Base class with a shared RestClient
├── JsonPlaceholderApiTests.cs   # Sample API tests
├── Clients/
│   └── JsonPlaceholderClient.cs # Example API client
├── Config/
│   ├── ConfigurationLoader.cs   # Loads appsettings + env vars
│   └── TestEnvironmentOptions.cs
├── appsettings.json
├── appsettings.Test.json        # Local overrides (not committed)
├── allureConfig.json
└── Dockerfile
```
