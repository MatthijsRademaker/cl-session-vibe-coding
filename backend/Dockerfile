# Use .NET SDK for development with hot reload support
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development

WORKDIR /app

# Copy solution and project files for dependency restoration
COPY ChatBot.sln ./
COPY Domain/Domain.csproj ./Domain/
COPY Application/Application.csproj ./Application/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY Api/Api.csproj ./Api/
COPY Tests/Tests.csproj ./Tests/

# Restore dependencies (this is cached unless .csproj files change)
RUN dotnet restore

# DON'T copy source code here - it comes from the volume mount
# This ensures the container always uses the latest code from your host

# Expose the HTTP port
EXPOSE 5000

# Set environment variables for development
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

# Run with hot reload using dotnet watch
WORKDIR /app/Api
CMD ["dotnet", "watch" ]
