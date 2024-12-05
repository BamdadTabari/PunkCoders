using DataProvider.Certain.Configs;
using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Repository;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PunkCoders.Configs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerDbConnection"))
           .EnableDetailedErrors();
});

// Add repositories and services
builder.Services.AddScoped<SecurityTokenConfig>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<TokenCleanupTask>();
builder.Services.AddScoped<JobScheduler>();

// Add Hangfire
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("ServerDbConnection")));
builder.Services.AddHangfireServer();

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Month)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

// Build the application
var app = builder.Build();

// Configure job scheduler after building the app
using (var scope = app.Services.CreateScope())
{
    var jobScheduler = scope.ServiceProvider.GetRequiredService<JobScheduler>();
    jobScheduler.ConfigureJobs();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Hangfire dashboard
app.UseHangfireDashboard("/hangfire");

app.UseAuthorization();
app.MapControllers();
app.Run();
