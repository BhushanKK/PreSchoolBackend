using MediatR;
using Microsoft.AspNetCore.Mvc;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;

public static class SchoolRegistrationApi
{
    public static IEndpointRouteBuilder MapSchoolRegistrationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/schoolregistration")
                       .WithTags("School Registration");

        group.MapGet("/", GetAll)
             .WithName("GetAllSchoolRegistrations")
             .WithSummary("Get all school registrations")
             .WithDescription("Returns all school registration records.")
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status500InternalServerError)
             .RequireAuthorization();

        group.MapGet("/{id:guid}", GetById)
             .WithName("GetSchoolRegistrationById")
             .WithSummary("Get school registration by Id")
             .WithDescription("Returns a school registration record by Id.")
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status404NotFound)
             .RequireAuthorization();

        group.MapPost("/", Create)
             .WithName("CreateSchoolRegistration")
             .WithSummary("Create School Registration")
             .WithDescription("Creates a new school registration record.")
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .RequireAuthorization();

        group.MapPut("/{id:guid}", Update)
             .WithName("UpdateSchoolRegistration")
             .WithSummary("Update School Registration")
             .WithDescription("Updates an existing school registration record.")
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .RequireAuthorization();

        group.MapDelete("/{id:guid}", Delete)
             .WithName("DeleteSchoolRegistration")
             .WithSummary("Delete School Registration")
             .WithDescription("Deletes a school registration record.")
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status404NotFound)
             .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        [AsParameters] PaginationRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllSchoolRegistrationQuery(request),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdSchoolRegistrationQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateSchoolRegistrationCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateSchoolRegistrationCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.SchoolRegistrationId = id;

        var result = await sender.Send(request, cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteSchoolRegistrationCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}