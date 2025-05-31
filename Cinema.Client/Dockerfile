# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Установка системных зависимостей
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
        libkrb5-dev \
        zlib1g-dev \
        libssl-dev \
        curl \
        wget \
        gnupg \
        git && \
    rm -rf /var/lib/apt/lists/*

# Очистка кэша
RUN dotnet nuget locals all --clear && \
    dotnet workload clean

# Копируем .csproj файлы
COPY ["Cinema.Client/Cinema.Client.csproj", "Cinema.Client/"]
COPY ["Cinema.Application/Cinema.Application.csproj", "Cinema.Application/"]
COPY ["Cinema.Domain/Cinema.Domain.csproj", "Cinema.Domain/"]

# Восстанавливаем зависимости
RUN dotnet restore "Cinema.Client/Cinema.Client.csproj"

# Копируем остальные файлы
COPY . .
WORKDIR "/src/Cinema.Client"

# Установка WebAssembly инструментов
ENV DOTNET_NOLOGO=true
ENV NUGET_XMLDOC_MODE=skip

# Сборка и публикация
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish