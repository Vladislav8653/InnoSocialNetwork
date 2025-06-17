# Базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 2000

# Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["UserService.Presentation/UserService.Presentation.csproj", "UserService.Presentation/"]
COPY ["UserService.Application/UserService.Application.csproj", "UserService.Application/"]
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]
RUN dotnet restore "UserService.Presentation/UserService.Presentation.csproj"
COPY . .
WORKDIR "/src/UserService.Presentation"
RUN dotnet build "UserService.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.Presentation.csproj" -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Presentation.dll"]
