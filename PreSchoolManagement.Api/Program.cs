using Serilog;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Application Services
builder.Services.AddMasterServices();
builder.Services.AddMediatRServices();
builder.Services.AddValidatorServices();
builder.Services.AddMapperServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<AuditMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

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
app.MapAuthEndpoints();

app.Run();