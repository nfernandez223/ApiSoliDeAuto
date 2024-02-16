FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app/src/ApiSoliDeAuto
COPY src/ApiSoliDeAuto/ApiSoliDeAuto.csproj .
RUN dotnet restore ApiSoliDeAuto.csproj
COPY src/Application ../Application
COPY src/Domain ../Domain
COPY src/InfraEstructure ../InfraEstructure
COPY src/ApiSoliDeAuto .
WORKDIR /app/src/ApiSoliDeAuto
RUN dotnet build ApiSoliDeAuto.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiSoliDeAuto.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiSoliDeAuto.dll"]