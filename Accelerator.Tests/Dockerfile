FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Accelerator.Tests/Accelerator.Tests.csproj", "Accelerator.Tests/"]
RUN dotnet restore "Accelerator.Tests/Accelerator.Tests.csproj"

COPY . .
WORKDIR "/src/Accelerator.Tests"
RUN dotnet build "Accelerator.Tests.csproj" -c Release
RUN dotnet test "Accelerator.Tests.csproj" -c Release --no-build --logger "console;verbosity=normal"
