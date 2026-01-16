FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG GITHUB_USERNAME
ARG GITHUB_TOKEN

WORKDIR /src

# Adicionar fonte do GitHub Packages
RUN dotnet nuget add source "https://nuget.pkg.github.com/TC-FIAP-Grupo-11/index.json" --name "TC-FIAP-Grupo-11" --username $GITHUB_USERNAME --password $GITHUB_TOKEN --store-password-in-clear-text

# Copiar projeto do Notifications
COPY ["FCG.Api.Notifications/src/FCG.Api.Notifications/FCG.Api.Notifications.csproj", "FCG.Api.Notifications/src/FCG.Api.Notifications/"]

RUN dotnet restore "FCG.Api.Notifications/src/FCG.Api.Notifications/FCG.Api.Notifications.csproj"

COPY . .
WORKDIR "/src/FCG.Api.Notifications/src/FCG.Api.Notifications"
RUN dotnet build "FCG.Api.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FCG.Api.Notifications.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.Api.Notifications.dll"]
