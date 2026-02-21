# Accelerator.ApiTests (API tests)

xUnit test project for API testing using [RestSharp](https://restsharp.dev/). Includes a base test class and a sample client for [JSONPlaceholder](https://jsonplaceholder.typicode.com/).

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Network access (tests call public APIs)

## Setup

1. Restore and build the project:

   ```bash
   dotnet restore Accelerator.ApiTests/Accelerator.ApiTests.csproj
   dotnet build Accelerator.ApiTests/Accelerator.ApiTests.csproj
   ```

## Running tests locally

From the repository root:

```bash
dotnet test Accelerator.ApiTests/Accelerator.ApiTests.csproj
```

Options:

```bash
# Verbose output
dotnet test Accelerator.ApiTests --logger "console;verbosity=detailed"

# Run a single test
dotnet test Accelerator.ApiTests --filter "FullyQualifiedName~GetPosts"
```

## Running with Docker

Tests run during the image build. From the repository root:

```bash
docker build -f Accelerator.ApiTests/Dockerfile -t accelerator-apitests .
```

Or with Compose:

```bash
docker compose build accelerator-apitests
```

The container build restores, builds, and runs the test suite. No extra services are required for the sample tests (they use the public JSONPlaceholder API).

## Project structure

- `BaseApiTest.cs` – Base class with a RestClient (optional for shared base URL)
- `Clients/JsonPlaceholderClient.cs` – Example API client
- `JsonPlaceholderApiTests.cs` – Sample tests
