FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Accelerator.ApiTests/Accelerator.ApiTests.csproj", "Accelerator.ApiTests/"]
RUN dotnet restore "Accelerator.ApiTests/Accelerator.ApiTests.csproj"

COPY . .
WORKDIR "/src/Accelerator.ApiTests"
RUN dotnet build "Accelerator.ApiTests.csproj" -c Release
RUN dotnet test "Accelerator.ApiTests.csproj" -c Release --no-build --logger "console;verbosity=normal"
