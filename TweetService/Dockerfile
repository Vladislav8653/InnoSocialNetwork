# Базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 2000

# Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем sln и все csproj'и (с сохранением структуры)
COPY TweetService.Domain/*.csproj TweetService.Domain/
COPY TweetService.Application/*.csproj TweetService.Application/
COPY TweetService.Infrastructure/*.csproj TweetService.Infrastructure/
COPY TweetService.Presentation/*.csproj TweetService.Presentation/
#COPY TweetService.Tests/*.csproj TweetService.Tests/

# Восстанавливаем зависимости
RUN dotnet restore TweetService.Presentation/TweetService.Presentation.csproj

# Копируем остальные файлы
COPY TweetService.Presentation/ TweetService.Presentation/
COPY TweetService.Infrastructure/ TweetService.Infrastructure/
COPY TweetService.Application/ TweetService.Application/
COPY TweetService.Domain/ TweetService.Domain/
#COPY TweetService.Tests/ TweetService.Tests/

# Сборка проекта
WORKDIR "TweetService.Presentation"
RUN dotnet build "TweetService.Presentation.csproj" -c Release -o /app/build

# Публикация
FROM build AS publish
RUN dotnet publish "TweetService.Presentation.csproj" -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TweetService.Presentation.dll"]
