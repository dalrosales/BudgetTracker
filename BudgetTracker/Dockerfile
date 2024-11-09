#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# Base stage with ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080 

# Build stage with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project and restore dependencies
COPY ["BudgetTracker/BudgetTracker.csproj", "BudgetTracker/"]
RUN dotnet restore "BudgetTracker/BudgetTracker.csproj"

# Copy and build the project files
COPY . .
WORKDIR "/src/BudgetTracker"
RUN dotnet build "BudgetTracker.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Debug # or Release
RUN dotnet publish "BudgetTracker.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# Final stage for the app runtime
FROM base AS final
WORKDIR /app

# Copy the published files
COPY --from=publish /app/publish .

# Entry point for .NET app
ENTRYPOINT ["dotnet", "BudgetTracker.dll"]
