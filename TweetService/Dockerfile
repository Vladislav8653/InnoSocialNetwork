# Базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 2000

# Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем sln и все исходники целиком
COPY . .

# Восстанавливаем зависимости
RUN dotnet restore TweetService.Presentation/TweetService.Presentation.csproj

# Публикация (включает сборку)
RUN dotnet publish TweetService.Presentation/TweetService.Presentation.csproj -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TweetService.Presentation.dll"]
