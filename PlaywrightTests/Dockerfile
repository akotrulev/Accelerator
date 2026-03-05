FROM mcr.microsoft.com/playwright/dotnet:v1.58.0-jammy AS build

WORKDIR /app

COPY Directory.Packages.props .
COPY TestCore/TestCore.csproj TestCore/
COPY PlaywrightTests/PlaywrightTests.csproj PlaywrightTests/
RUN dotnet restore PlaywrightTests/PlaywrightTests.csproj

COPY TestCore/ TestCore/
COPY PlaywrightTests/ PlaywrightTests/

RUN dotnet build PlaywrightTests/PlaywrightTests.csproj --no-restore -c Release

# Mount /app/failures, /app/videos, /app/playwright-traces, and /app/allure-results to retrieve artifacts.
CMD ["dotnet", "test", "PlaywrightTests/PlaywrightTests.csproj", "--no-build", "-c", "Release"]
