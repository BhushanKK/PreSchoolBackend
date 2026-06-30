using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SchoolAdmission.Application.Features.CasteMasters.Commands;
using SchoolAdmission.Api.Endpoints;
using SchoolAdmission.Api.Extensions;
using SchoolAdmission.Application.Features.Masters.CasteMaster.Mappings;
using SchoolAdmission.Application.Features.Masters.CasteMaster.Validators;
using SchoolAdmission.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMasterServices();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCasteMasterCommand).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<CreateCasteMasterCommandValidator>();
builder.Services.AddAutoMapper(typeof(CasteMasterProfile));

var app = builder.Build();

app.MapCasteMasterEndpoints();
app.MapOpenApi();
app.MapScalarApiReference();

app.Run();
