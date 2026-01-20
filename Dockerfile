FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar projeto do Notifications
COPY ["src/FCG.Api.Notifications/FCG.Api.Notifications.csproj", "src/FCG.Api.Notifications/"]

RUN dotnet restore "src/FCG.Api.Notifications/FCG.Api.Notifications.csproj"

COPY . .
WORKDIR "/src/src/FCG.Api.Notifications"
RUN dotnet build "FCG.Api.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FCG.Api.Notifications.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.Api.Notifications.dll"]
