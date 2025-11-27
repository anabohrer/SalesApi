# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY SalesSolution.sln ./
COPY src/SalesApi/SalesApi.csproj ./src/SalesApi/
COPY tests/SalesApi.Tests/SalesApi.Tests.csproj ./tests/SalesApi.Tests/

# Restore dependencies
RUN dotnet restore SalesSolution.sln

# Copy the rest of the source code
COPY src/SalesApi/ ./src/SalesApi/

# Build the application
WORKDIR /src/src/SalesApi
RUN dotnet build SalesApi.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
WORKDIR /src/src/SalesApi
RUN dotnet publish SalesApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# Test stage (optional - run with --target test)
FROM build AS test
WORKDIR /src
COPY tests/SalesApi.Tests/ ./tests/SalesApi.Tests/
RUN dotnet test SalesSolution.sln --logger "trx;LogFileName=test-results.trx" --logger "console;verbosity=detailed" --no-restore --results-directory /testresults

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080

# Run the application
ENTRYPOINT ["dotnet", "SalesApi.dll"]
