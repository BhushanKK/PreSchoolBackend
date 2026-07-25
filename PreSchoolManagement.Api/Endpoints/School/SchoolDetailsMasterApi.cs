using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Api.Endpoints;

public static class SchoolDetailsMasterApi
{
    public static IEndpointRouteBuilder MapSchoolDetailsMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/schooldetailsmaster")
                        .WithTags("School Details Master");

        group.MapGet("/", GetAll)
              .WithName("GetAllSchoolDetails")
              .WithSummary("Get all school details")
              .WithDescription("Returns all school details records.")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status500InternalServerError)
              .RequireAuthorization();

        group.MapGet("/{id:guid}", GetById)
              .WithName("GetSchoolDetailsById")
              .WithSummary("Get school details by Id")
              .WithDescription("Returns a school details record by Id.")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .RequireAuthorization();

        group.MapPost("/", Create)
              .WithName("CreateSchoolDetails")
              .WithSummary("Create School Details")
              .WithDescription("Creates a new school details record.")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .RequireAuthorization();

        group.MapPut("/{id:guid}", Update)
              .WithName("UpdateSchoolDetails")
              .WithSummary("Update School Details")
              .WithDescription("Updates an existing school details record.")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .RequireAuthorization();

        group.MapDelete("/{id:guid}", Delete)
              .WithName("DeleteSchoolDetails")
              .WithSummary("Delete School Details")
              .WithDescription("Deletes a school details record.")
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
            new GetAllSchoolDetailsMasterQuery(request),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdSchoolDetailsMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateSchoolDetailsMasterCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateSchoolDetailsMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.SchoolDetailsId = id;

        var result = await sender.Send(request, cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteSchoolDetailsMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}