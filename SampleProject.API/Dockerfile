#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SampleProject.API/SampleProject.API.csproj", "SampleProject.API/"]
COPY ["BuildingBlocks/BuildingBlocks.API/BuildingBlocks.API.csproj", "BuildingBlocks/BuildingBlocks.API/"]
COPY ["BuildingBlocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Resources/BuildingBlocks.Resources.csproj", "BuildingBlocks/BuildingBlocks.Resources/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
COPY ["SampleProject.Application/SampleProject.Application.csproj", "SampleProject.Application/"]
COPY ["SampleProject.Domain/SampleProject.Domain.csproj", "SampleProject.Domain/"]
COPY ["SampleProject.Resources/SampleProject.Resources.csproj", "SampleProject.Resources/"]
COPY ["SampleProject.Infrastructure/SampleProject.Infrastructure.csproj", "SampleProject.Infrastructure/"]
RUN dotnet restore "./SampleProject.API/SampleProject.API.csproj"
COPY . .
WORKDIR "/src/SampleProject.API"
RUN dotnet build "./SampleProject.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SampleProject.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleProject.API.dll"]