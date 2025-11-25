using SalesApi;
using SalesApi.Application;
using SalesApi.Domain.Services;
using SalesApi.Infrastructure.Csv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerFileOperationFilter>();

    options.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddScoped<ISalesDataSource, CsvSalesDataSource>();
builder.Services.AddScoped<ISalesSummaryUseCase, SalesSummaryUseCase>();

builder.Services.AddSingleton<IMedianCalculator, MedianCalculator>();
builder.Services.AddSingleton<IDateRangeCalculator, DateRangeCalculator>();
builder.Services.AddSingleton<IRegionAnalyzer, RegionAnalyzer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();