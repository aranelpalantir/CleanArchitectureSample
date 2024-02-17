#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/CleanArchSample.Api/CleanArchSample.Api.csproj", "Presentation/CleanArchSample.Api/"]
COPY ["Infrastructure/CleanArchSample.Infrastructure/CleanArchSample.Infrastructure.csproj", "Infrastructure/CleanArchSample.Infrastructure/"]
COPY ["Core/CleanArchSample.Application/CleanArchSample.Application.csproj", "Core/CleanArchSample.Application/"]
COPY ["Core/CleanArchSample.Domain/CleanArchSample.Domain.csproj", "Core/CleanArchSample.Domain/"]
COPY ["Infrastructure/CleanArchSample.Persistence/CleanArchSample.Persistence.csproj", "Infrastructure/CleanArchSample.Persistence/"]
RUN dotnet restore "./Presentation/CleanArchSample.Api/./CleanArchSample.Api.csproj"
COPY . .
WORKDIR "/src/Presentation/CleanArchSample.Api"
RUN dotnet build "./CleanArchSample.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArchSample.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchSample.Api.dll"]