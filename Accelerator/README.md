# Accelerator (main app)

.NET 8 console application. Includes references for Appium, Playwright, RestSharp, and Selenium for automation and API usage.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Setup

1. Clone the repository and open the solution root.
2. Restore dependencies:

   ```bash
   dotnet restore Accelerator/Accelerator.csproj
   ```

3. Build:

   ```bash
   dotnet build Accelerator/Accelerator.csproj
   ```

## Running locally

From the repository root:

```bash
dotnet run --project Accelerator
```

Or from the project directory:

```bash
cd Accelerator
dotnet run
```

## Running with Docker

From the repository root:

```bash
# Build image
docker build -f Accelerator/Dockerfile -t accelerator .

# Run container
docker run --rm accelerator
```

Or use Docker Compose:

```bash
docker compose build accelerator
docker compose up accelerator
```

## Project structure

- `Program.cs` – Application entry point
- `Appium/` – Appium automation (iOS simulator & Android emulator config, driver factory, base page)
