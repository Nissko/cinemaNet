FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cinema.API/Cinema.API.csproj", "Cinema.API/"]
COPY ["Cinema.Application/Cinema.Application.csproj", "Cinema.Application/"]
COPY ["Cinema.Domain/Cinema.Domain.csproj", "Cinema.Domain/"]
COPY ["Cinema.Infrastructure/Cinema.Infrastructure.csproj", "Cinema.Infrastructure/"]
RUN dotnet restore "Cinema.API/Cinema.API.csproj"
COPY . .
WORKDIR "/src/Cinema.API"
RUN dotnet build "./Cinema.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cinema.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cinema.API.dll"]
