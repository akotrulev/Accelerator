# Accelerator

Solution containing Playwright UI tests, RestSharp API tests, and Appium mobile tests, backed by a shared `TestCore` library.

## Projects

| Project | Description |
|---------|-------------|
| `PlaywrightTests` | Playwright browser (UI) tests |
| `ApiTests` | RestSharp API tests |
| `AppiumTests` | Appium mobile tests (Android / iOS, local or BrowserStack) |
| `TestCore` | Shared logging, configuration, and base options |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/) (optional, for containerised runs)
- [Node.js + Appium 2](https://appium.io/docs/en/latest/quickstart/) (for local Appium runs)
- [PowerShell](https://github.com/PowerShell/PowerShell) (for Playwright browser installation)

## Quick start

```bash
# Restore and build entire solution
dotnet restore
dotnet build

# Run UI tests (Playwright)
dotnet test PlaywrightTests/PlaywrightTests.csproj

# Run API tests
dotnet test ApiTests/ApiTests.csproj

# Run mobile tests (Appium, local Android emulator)
EXECUTION_MODE=Local TARGET_PLATFORM=Android dotnet test AppiumTests/AppiumTests.csproj
```

## Docker Compose

From the repository root:

```bash
# Build all images
docker compose build

# Run UI tests (artifacts written to mounted volumes)
docker compose up playwright-tests

# Run API tests
docker compose up api-tests
```

## Configuration

Each project reads configuration from:

- **`appsettings.json`** – Default URLs and settings (committed).
- **`appsettings.<ENV>.json`** – Optional per-environment override loaded when `TEST_ENV=<ENV>` is set.
- **Environment variables** – Override any JSON value at runtime.

### PlaywrightTests — `PlaywrightSettings` section

| Key | Default | Description |
|-----|---------|-------------|
| `BrowserType` | `chromium` | `chromium`, `firefox`, or `webkit` |
| `Headless` | `false` | Run headless |
| `ViewportWidth` / `ViewportHeight` | `1920` / `1080` | Browser viewport |
| `SlowMo` | `0` | Slow-motion delay in ms (0 = off) |
| `DefaultTimeoutMs` | `0` | Global action timeout in ms (0 = Playwright default) |
| `RecordVideo` | `true` | Record video (set `false` to skip for local debugging) |

### AppiumTests — environment variables

| Variable | Values | Description |
|----------|--------|-------------|
| `EXECUTION_MODE` | `Local` (default), `BrowserStack` | Where to run |
| `TARGET_PLATFORM` | `Android` (default), `iOS` | Target mobile platform |
| `TEST_TYPE` | `Browser` (default), `NativeApp` | Browser or native app session |
| `APP_PATH` | path string | Path to `.apk`/`.ipa` for local native runs |

### BrowserStack

Set `EXECUTION_MODE=BrowserStack` and provide credentials via environment variables or `appsettings.json`:

```json
{
  "BrowserStack": {
    "UserName": "...",
    "AccessKey": "...",
    "HubUrl": "https://hub-cloud.browserstack.com/wd/hub"
  }
}
```
