FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Packages.props .
COPY ["TestCore/TestCore.csproj", "TestCore/"]
COPY ["ApiTests/ApiTests.csproj", "ApiTests/"]
RUN dotnet restore "ApiTests/ApiTests.csproj"

COPY TestCore/ TestCore/
COPY ApiTests/ ApiTests/
WORKDIR "/src/ApiTests"
RUN dotnet build "ApiTests.csproj" -c Release --no-restore

# Mount /src/allure-results to retrieve Allure results.
CMD ["dotnet", "test", "ApiTests.csproj", "-c", "Release", "--no-build", "--logger", "console;verbosity=normal"]
