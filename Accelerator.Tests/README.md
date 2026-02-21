# Accelerator.Tests (Playwright UI tests)

xUnit test project for browser automation using [Playwright](https://playwright.dev/dotnet/) with a Page Object Model (POM) structure.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Playwright browsers** (required for running tests)

## Setup

1. Restore and build the project:

   ```bash
   dotnet restore Accelerator.Tests/Accelerator.Tests.csproj
   dotnet build Accelerator.Tests/Accelerator.Tests.csproj
   ```

2. Install Playwright browsers (one-time):

   ```bash
   pwsh -File Accelerator.Tests/bin/Debug/net8.0/playwright.ps1 install
   ```

   If PowerShell Core (`pwsh`) is not installed:

   - **macOS:** `brew install powershell`
   - Or install via Node: `npx playwright install` (browsers are shared with .NET when using the default cache)

## Running tests locally

From the repository root:

```bash
dotnet test Accelerator.Tests/Accelerator.Tests.csproj
```

Options:

```bash
# Verbose output
dotnet test Accelerator.Tests --logger "console;verbosity=detailed"

# Run a single test
dotnet test Accelerator.Tests --filter "FullyQualifiedName~HomePage_Loads"
```

## Running with Docker

Tests run during the image build. From the repository root:

```bash
docker build -f Accelerator.Tests/Dockerfile -t accelerator-tests .
```

Or with Compose:

```bash
docker compose build accelerator-tests
```

**Note:** The default Dockerfile does not install Playwright browsers. For real browser runs in Docker, use a [Playwright Docker image](https://playwright.dev/dotnet/docs/docker) or add browser installation to the Dockerfile.

## Project structure

- `BaseTest.cs` – Base class with Playwright/browser setup (xUnit `IAsyncLifetime`)
- `Pages/BasePage.cs` – Base page object with common locator helpers
- `Pages/HomePage.cs` – Example page object
- `HomePageTests.cs` – Sample tests
