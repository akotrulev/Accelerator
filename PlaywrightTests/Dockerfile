FROM mcr.microsoft.com/playwright/dotnet:v1.58.0-jammy AS build

WORKDIR /app

COPY PlaywrightTests/PlaywrightTests.csproj PlaywrightTests/
RUN dotnet restore PlaywrightTests/PlaywrightTests.csproj

COPY PlaywrightTests/ PlaywrightTests/

RUN dotnet build PlaywrightTests/PlaywrightTests.csproj --no-restore -c Release

CMD ["dotnet", "test", "PlaywrightTests/PlaywrightTests.csproj", "--no-build", "-c", "Release"]
