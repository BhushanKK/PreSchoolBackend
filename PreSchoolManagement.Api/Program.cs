using Serilog;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SchoolAdmission.Api.Endpoints;
using SchoolAdmission.Api.Extensions;
using SchoolAdmission.Api.Middlewares;
using SchoolAdmission.Infrastructure.Data;
using SchoolManagement.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AuditContext>();

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

var logDirectory = Path.Combine(builder.Environment.ContentRootPath, "Logs");

if (!Directory.Exists(logDirectory))
    Directory.CreateDirectory(logDirectory);

DeleteOldLogFiles(logDirectory, TimeSpan.FromDays(7));

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// OpenApi
builder.Services.AddOpenApi();

// Application Services
builder.Services.AddMasterServices();
builder.Services.AddMediatRServices();
builder.Services.AddValidatorServices();
builder.Services.AddMapperServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<AuditMiddleware>();

// OpenApi
app.MapOpenApi();

// Scalar
app.MapScalarApiReference(options =>
{
    options.WithTitle("School Admission API");
    options.WithTheme(ScalarTheme.BluePlanet);
    options.WithOpenApiRoutePattern("/openapi/{documentName}.json");
});

// Endpoints
app.MapCasteMasterEndpoints();

app.Run();

static void DeleteOldLogFiles(string logDirectory, TimeSpan retentionPeriod)
{
    if (!Directory.Exists(logDirectory))
        return;

    var cutoffTime = DateTime.UtcNow.Subtract(retentionPeriod);

    foreach (var file in Directory.EnumerateFiles(logDirectory, "*.log", SearchOption.TopDirectoryOnly))
    {
        if (File.GetLastWriteTimeUtc(file) < cutoffTime)
        {
            try
            {
                File.Delete(file);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to delete old log file: {FilePath}", file);
            }
        }
    }
}