using Serilog;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SchoolAdmission.Api.Endpoints;
using SchoolAdmission.Api.Extensions;
using SchoolAdmission.Api.Middlewares;
using SchoolAdmission.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonServices();

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

var logDirectory = LogFileCleanupExtensions.EnsureLogDirectory(builder.Environment.ContentRootPath);

LogFileCleanupExtensions.DeleteOldLogFiles(logDirectory, TimeSpan.FromDays(7));

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