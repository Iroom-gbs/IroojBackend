FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IroojBackend/IroojBackend.csproj", "IroojBackend/"]
RUN dotnet restore "IroojBackend/IroojBackend.csproj"
COPY . .
WORKDIR "/src/IroojBackend"
RUN dotnet build "IroojBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IroojBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IroojBackend.dll"]
