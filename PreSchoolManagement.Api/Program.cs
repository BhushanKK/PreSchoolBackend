using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SchoolAdmission.Api.Endpoints;
using SchoolAdmission.Api.Extensions;
using SchoolAdmission.Application.Features.CasteMasters.Commands;
using SchoolAdmission.Application.Features.Masters.CasteMaster.Mappings;
using SchoolAdmission.Application.Features.Masters.CasteMaster.Validators;
using SchoolAdmission.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// OpenApi
builder.Services.AddOpenApi();

// Application Services
builder.Services.AddMasterServices();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCasteMasterCommand).Assembly));

builder.Services.AddValidatorsFromAssemblyContaining<CreateCasteMasterCommandValidator>();

builder.Services.AddAutoMapper(typeof(CasteMasterProfile));

var app = builder.Build();

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