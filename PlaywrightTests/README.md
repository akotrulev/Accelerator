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

## Test artifacts & report

On failure each test saves three artifacts:

| Artifact | Location | Description |
|----------|----------|-------------|
| Screenshot | `failures/<TestName>.png` | Full-page PNG captured at the moment of failure |
| Video | `videos/` | WebM recording of the entire test run |
| Playwright Trace | `playwright-traces/<ClassName>.<TestName>.zip` | Playwright's built-in report — action timeline, DOM snapshots, network, console logs |

### Viewing the trace (Playwright's report)

```bash
# After a local run
pwsh PlaywrightTests/bin/Release/net8.0/playwright.ps1 show-trace \
  playwright-traces/<ClassName>.<TestName>.zip
```

Or drag-and-drop the zip onto **[trace.playwright.dev](https://trace.playwright.dev)** — all processing happens in-browser, nothing is uploaded externally.

### GitHub Actions

Download the **`playwright-failure-artifacts`** artifact from the workflow run — it contains `failures/`, `videos/`, and `playwright-traces/`.

### Docker

```bash
docker build -f PlaywrightTests/Dockerfile -t playwright-tests .

docker run --rm \
  -v $(pwd)/failures:/app/failures \
  -v $(pwd)/videos:/app/videos \
  -v $(pwd)/playwright-traces:/app/playwright-traces \
  playwright-tests
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
└── Dockerfile
```
