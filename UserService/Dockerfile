# Базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 2000

# Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем sln и все csproj'и (с сохранением структуры)
COPY UserService.Domain/*.csproj UserService.Domain/
COPY UserService.Application/*.csproj UserService.Application/
COPY UserService.Infrastructure/*.csproj UserService.Infrastructure/
COPY UserService.Presentation/*.csproj UserService.Presentation/
COPY UserService.Tests/*.csproj UserService.Tests/

# Восстанавливаем зависимости
RUN dotnet restore UserService.Presentation/UserService.Presentation.csproj

# Копируем остальные файлы
COPY UserService.Presentation/ UserService.Presentation/
COPY UserService.Infrastructure/ UserService.Infrastructure/
COPY UserService.Application/ UserService.Application/
COPY UserService.Domain/ UserService.Domain/
COPY UserService.Tests/ UserService.Tests/

# Сборка проекта
WORKDIR "UserService.Presentation"
RUN dotnet build "UserService.Presentation.csproj" -c Release -o /app/build

# Публикация
FROM build AS publish
RUN dotnet publish "UserService.Presentation.csproj" -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Presentation.dll"]
