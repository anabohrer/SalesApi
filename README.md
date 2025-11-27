# SalesApi

A .NET 10 Web API for processing and analyzing sales data from CSV files. Upload sales records and receive statistical summaries including median costs, regional distribution, date ranges, and revenue totals.

## Features

- 📊 **Statistical Analysis**: Compute median unit costs, identify top regions, calculate date ranges
- 📁 **CSV Processing**: Stream-based parsing for efficient handling of large datasets (100K+ records)
- 🏗️ **Clean Architecture**: Separation of concerns with Application, Domain, and Infrastructure layers
- 🧪 **Comprehensive Testing**: Unit, integration, and controller tests with high coverage
- 📝 **Swagger UI**: Interactive API documentation with file upload support
- ⚡ **Async Processing**: Full async/await implementation with cancellation token support

## Technology Stack

- **.NET 10.0** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **CsvHelper 33.1.0** - CSV parsing and mapping
- **Swashbuckle.AspNetCore 10.0.1** - OpenAPI/Swagger documentation
- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Fluent test assertions

## Project Structure

```
src/SalesApi/
├── Controllers/          # API endpoints
├── Application/          # Business logic use cases
├── Domain/              # Core models and services
│   ├── Models/         # SalesRecord, SummaryResult
│   └── Services/       # MedianCalculator, DateRangeCalculator
├── Infrastructure/      # External integrations
│   └── Csv/           # CSV data source implementation
└── Data/               # Sample CSV files

tests/SalesApi.Tests/
├── Controllers/         # Controller tests
├── Application/         # Use case tests
├── Domain/             # Domain service tests
└── Integration/        # CSV integration tests
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- IDE: Visual Studio 2022, VS Code, or Rider
- **Docker** (optional, for containerized deployment)

### Running the Application

#### Option 1: Using .NET CLI

```bash
# Clone the repository
git clone https://github.com/anabohrer/SalesApi.git
cd SalesApi

# Build the solution
dotnet build

# Run the API
cd src/SalesApi
dotnet run

# Access Swagger UI
# Navigate to: https://localhost:7093/swagger
```

#### Option 2: Using Docker

```bash
# Build the Docker image
docker build -t salesapi:latest .

# Run the container
docker run -p 8080:8080 salesapi:latest

# Or run in detached mode with a custom name
docker run -d -p 8080:8080 --name salesapi salesapi:latest

# Access the API
# Navigate to: http://localhost:8080/swagger
```

**Docker Commands:**
```bash
# View running containers
docker ps

# View logs
docker logs salesapi

# Stop the container
docker stop salesapi

# Remove the container
docker rm salesapi
```

### Running Tests

```bash
# Run all tests locally
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run tests in Docker (with full output)
docker build --no-cache --progress=plain --target test -t salesapi:test .

# Run tests in Docker (cached, faster)
docker build --target test -t salesapi:test .

# Extract test results from Docker
docker create --name temp-test salesapi:test
docker cp temp-test:/testresults ./test-results
docker rm temp-test
```

## API Endpoints

### POST `/api/Sales/summary`

Upload a CSV file and receive sales summary statistics.

**Request:**
- Content-Type: `multipart/form-data`
- Parameter: `file` (CSV file)

**Response (200 OK):**
```json
{
  "medianUnitCost": 152.50,
  "mostCommonRegion": "Sub-Saharan Africa",
  "firstOrderDate": "2010-01-04T00:00:00",
  "lastOrderDate": "2017-12-28T00:00:00",
  "daysBetweenOrders": 2915,
  "totalRevenue": 123456789.50
}
```

**Error Responses:**
- `400 Bad Request` - File is null, empty, or invalid format

## CSV Format

Expected CSV structure:

```csv
Region,Country,Item Type,Sales Channel,Order Priority,Order Date,Order ID,Ship Date,Units Sold,Unit Price,Unit Cost,Total Revenue,Total Cost,Total Profit
Sub-Saharan Africa,Chad,Office Supplies,Offline,M,1/27/2011,292494523,2/12/2011,4484,651.21,524.96,2920025.64,2353920.64,566105.00
```

**Required Columns:**
- Region, Country, Item Type, Sales Channel, Order Priority
- Order Date, Order ID, Ship Date
- Units Sold, Unit Price, Unit Cost
- Total Revenue, Total Cost, Total Profit

**Supported Date Formats:**
- M/d/yyyy, MM/dd/yyyy, d/M/yyyy
- yyyy-MM-dd, yyyy/MM/dd

## Architecture

### Clean Architecture Layers

1. **Controllers**: Handle HTTP requests, validation, and response formatting
2. **Application**: Orchestrate business logic through use cases
3. **Domain**: Core business entities and pure calculation logic
4. **Infrastructure**: External concerns like CSV parsing

### Dependency Injection

```csharp
// Scoped (per-request)
builder.Services.AddScoped<ISalesDataSource, CsvSalesDataSource>();
builder.Services.AddScoped<ISalesSummaryUseCase, SalesSummaryUseCase>();

// Singleton (stateless services)
builder.Services.AddSingleton<IMedianCalculator, MedianCalculator>();
builder.Services.AddSingleton<IDateRangeCalculator, DateRangeCalculator>();
```

## Development

### Adding New Features

1. **Add Domain Logic**: Create interfaces and implementations in `Domain/`
2. **Create Use Cases**: Implement business workflows in `Application/`
3. **Add Controllers**: Expose functionality via HTTP endpoints
4. **Write Tests**: Add unit and integration tests

### Code Style

- **Nullable Reference Types**: Enabled throughout
- **Primary Constructors**: Used for dependency injection
- **Sealed Classes**: Applied to prevent unintended inheritance
- **Async/Await**: Used for all I/O operations

## Testing

The project follows the **BDD (Behavior-Driven Development)** approach with **Given-When-Then** pattern for test naming and structure:

```csharp
[Fact]
public void GivenOddCount_WhenComputingMedian_ThenMiddleValueReturned()
{
    // Given - Context/Preconditions
    var values = new[] { 10m, 30m, 97.44m };
    
    // When - Action/Behavior
    var result = medianCalculator.ComputeMedian(values);
    
    // Then - Expected Result/Assertion
    result.Should().Be(30m);
}
```

**Test Structure:**
- **Given** - Sets up the context and preconditions
- **When** - Executes the action or behavior being tested
- **Then** - Verifies the expected outcome

## Contact

Ana Luiza Bohrer - [@anabohrer](https://github.com/anabohrer)

Project Link: [https://github.com/anabohrer/SalesApi](https://github.com/anabohrer/SalesApi)
