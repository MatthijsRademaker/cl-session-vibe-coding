# Use .NET SDK for development with hot reload support
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development

WORKDIR /app

# Copy solution and project files
COPY ChatBot.sln ./
COPY Domain/Domain.csproj ./Domain/
COPY Application/Application.csproj ./Application/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY Api/Api.csproj ./Api/
COPY Tests/Tests.csproj ./Tests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Expose the HTTP port
EXPOSE 5000

# Set environment variables for development
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5000

# Run with hot reload using dotnet watch
WORKDIR /app/Api
CMD ["dotnet", "watch", "run", "--no-launch-profile"]
