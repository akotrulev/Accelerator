# PlaywrightTests (UI tests)

NUnit test project for browser UI testing using [Playwright](https://playwright.dev/dotnet/). Includes a base test class, page object helpers, and a sample home-page test.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Playwright browsers installed (see Setup below)

## Setup

1. Restore and build the project:

   ```bash
   dotnet restore PlaywrightTests/PlaywrightTests.csproj
   dotnet build PlaywrightTests/PlaywrightTests.csproj
   ```

2. Install Playwright browsers (required once per machine):

   ```bash
   pwsh PlaywrightTests/bin/Debug/net8.0/playwright.ps1 install
   ```

   If you don't have PowerShell, install it via `brew install --cask powershell` on macOS.

## Configuration

Edit `PlaywrightTests/appsettings.json` (or create `appsettings.Test.json` to override without modifying the committed file):

```json
{
  "TestEnvironment": {
    "BaseUrl": "https://example.com"
  }
}
```

Environment variables also override JSON values (e.g. `TestEnvironment__BaseUrl=https://staging.example.com`).

## Running tests locally

From the repository root:

```bash
dotnet test PlaywrightTests/PlaywrightTests.csproj
```

Options:

```bash
# Verbose output
dotnet test PlaywrightTests --logger "console;verbosity=detailed"

# Run a single test
dotnet test PlaywrightTests --filter "FullyQualifiedName~HomePageTests"

# Run headed (non-headless) — set Headless = false in BaseTest.cs
dotnet test PlaywrightTests
```

## Running with Docker

From the repository root:

```bash
docker build -f PlaywrightTests/Dockerfile -t playwright-tests .
```

The image is based on `mcr.microsoft.com/playwright/dotnet:v1.58.0-jammy`, which ships with all Playwright browsers pre-installed. Tests run automatically as the container starts.

## Allure reports

After a test run, results are written to `allure-results/`. Generate and open the report with:

```bash
allure serve allure-results
```

## Project structure

```
PlaywrightTests/
├── BaseTest.cs                  # [SetUp]/[TearDown] — launches/closes Playwright browser
├── HomePageTests.cs             # Sample UI test
├── Config/
│   ├── ConfigurationLoader.cs   # Loads appsettings + env vars
│   └── TestEnvironmentOptions.cs
├── Pages/
│   ├── BasePage.cs              # Sync wrappers over Playwright async API
│   └── HomePage.cs              # Home page object
├── appsettings.json
├── appsettings.Test.json        # Local overrides (not committed)
├── allureConfig.json
└── Dockerfile
```
