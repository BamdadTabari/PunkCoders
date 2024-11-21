using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ServerDbConnection")).EnableDetailedErrors();
});
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


builder.Host.UseSerilog(); // Replace default logging with Serilog


builder.Host.UseSerilog();  // Use Serilog as the logging provider

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Punk");
app.UseHttpsRedirection();
app.MapGet("/", () =>
{
    app.Logger.LogInformation("Hello from Serilog with Seq!");
    return "Hello, World!";
});

app.UseAuthorization();

app.MapControllers();

app.Run();
