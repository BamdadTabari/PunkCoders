using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PunkCoders.Configs;
using Serilog;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Stores;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// configure api explorer for swagger
builder.Services.AddEndpointsApiExplorer();

// Add Swagger json generator
builder.Services.AddSwaggerGen();

// Add repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ServerDbConnection")).EnableDetailedErrors();
});

// Add CORS policy
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("Punk", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});
// Configure Serilog to log to Seq
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Month)
    .Enrich.FromLogContext() // Enrich logs with context
    .CreateLogger();

// Replace default logging with Serilog
builder.Host.UseSerilog();

// Add in-memory caching service
builder.Services.AddMemoryCache();
builder.Services.Configure<CacheOptions>(options =>
{
    options.AbsoluteExpiration = TimeSpan.FromMinutes(10); // Global absolute expiration
    options.SlidingExpiration = TimeSpan.FromMinutes(5);   // Global sliding expiration
});

// image optimizer service config
builder.Services.AddSingleton<ImageProcessingService>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable OpenAPI
    app.MapOpenApi();
    // Enable Swagger
    app.UseSwagger();
    // Enable Swagger UI
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("Punk");

// Configure the HTTP request pipeline. (redirect http to https)
app.UseHttpsRedirection();

//// Configure the HTTP request pipeline.
//app.MapGet("/", () =>
//{
//    app.Logger.LogInformation("Hello from Serilog with Seq!");
//    return "Hello, World!";
//});

// Set up TusDotNet options
app.MapTus("/files", ctx => Task.FromResult(new DefaultTusConfiguration
{
    Store = new TusDiskStore(Path.Combine(Directory.GetCurrentDirectory(), "tus-temp")),
    UrlPath = "/files",
    MaxAllowedUploadSizeInBytes = 20 * 1024 * 1024 // 20 MB limit
}));
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "optimized")),
    RequestPath = "/optimized"
});
app.UseAuthorization();

app.MapControllers();

app.Run();
