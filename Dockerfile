#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Taskify.API/Taskify.API.csproj", "Taskify.API/"]
COPY ["Taskify.Application/Taskify.Application.csproj", "Taskify.Application/"]
COPY ["Taskify.Core/Taskify.Core.csproj", "Taskify.Core/"]
COPY ["Taskify.DataAccess/Taskify.DataAccess.csproj", "Taskify.DataAccess/"]
COPY ["Taskify.Infrastructure/Taskify.Infrastructure.csproj", "Taskify.Infrastructure/"]
RUN dotnet restore "./Taskify.API/Taskify.API.csproj"
COPY . .
WORKDIR "/src/Taskify.API"
RUN dotnet build "./Taskify.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Taskify.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taskify.API.dll"]