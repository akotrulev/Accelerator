# Accelerator

Solution containing the main application, Playwright UI tests, and RestSharp API tests.

## Projects

| Project | Description |
|---------|-------------|
| [Accelerator](Accelerator/README.md) | Main .NET 8 console application |
| [Accelerator.Tests](Accelerator.Tests/README.md) | Playwright browser (UI) tests |
| [Accelerator.ApiTests](Accelerator.ApiTests/README.md) | RestSharp API tests |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/) (optional, for containerized run)

## Quick start

```bash
# Restore and build entire solution
dotnet restore
dotnet build

# Run main app
dotnet run --project Accelerator

# Run UI tests
dotnet test Accelerator.Tests

# Run API tests
dotnet test Accelerator.ApiTests
```

## Docker Compose

From the repository root:

```bash
# Build all images
docker compose build

# Run the main application
docker compose up accelerator

# Build and run tests (tests execute during image build)
docker compose build accelerator-tests
docker compose build accelerator-apitests
```

## Configuration

Each project has test-environment configuration:

- **appsettings.json** – Default URLs and optional Username/Password (committed).
- **appsettings.Test.json** – Optional override per environment (e.g. different BaseUrl, ApiUri, credentials). Copy from the template in each project and set values as needed. Add `**/appsettings.Test.json` to `.gitignore` if you store real credentials there.

Configuration section: `TestEnvironment` with `BaseUrl`, `ApiUri`, `Username`, `Password`. Environment variables override JSON values.

See each project's README for detailed setup and run instructions.
