FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ClientTester/ClientTester.csproj", "ClientTester/"]
RUN dotnet restore "ClientTester/ClientTester.csproj"
COPY . .
WORKDIR "/src/ClientTester"
RUN dotnet build "ClientTester.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientTester.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientTester.dll"]
