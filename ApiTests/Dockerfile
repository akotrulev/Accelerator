FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ApiTests/ApiTests.csproj", "ApiTests/"]
RUN dotnet restore "ApiTests/ApiTests.csproj"

COPY . .
WORKDIR "/src/ApiTests"
RUN dotnet build "ApiTests.csproj" -c Release
RUN dotnet test "ApiTests.csproj" -c Release --no-build --logger "console;verbosity=normal"
