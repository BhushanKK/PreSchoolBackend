using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Api.Endpoints;

public static class SchoolStandardMappingApi
{
    public static IEndpointRouteBuilder MapSchoolStandardMappingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/SchoolStandardMapping")
                       .WithTags("School Standard Mapping");

        group.MapGet("/", GetAll)
            .WithName("GetAllSchoolStandardMappings")
            .WithSummary("Get all paginated School Standard Mapping records")
            .WithDescription("Returns paginated School Standard Mapping records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:guid}", GetById)
            .WithName("GetSchoolStandardMappingById")
            .WithSummary("Get School Standard Mapping by Id")
            .WithDescription("Returns a School Standard Mapping record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateSchoolStandardMapping")
            .WithSummary("Create School Standard Mapping")
            .WithDescription("Creates a new School Standard Mapping record.")
            .RequireAuthorization();

        group.MapPut("/{id:guid}", Update)
            .WithName("UpdateSchoolStandardMapping")
            .WithSummary("Update School Standard Mapping")
            .WithDescription("Updates an existing School Standard Mapping record.")
            .RequireAuthorization();

        group.MapDelete("/{id:guid}", Delete)
            .WithName("DeleteSchoolStandardMapping")
            .WithSummary("Delete School Standard Mapping")
            .WithDescription("Deletes a School Standard Mapping record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> Create(
        CreateSchoolStandardMappingCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetAll(
        [AsParameters] PaginationRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllSchoolStandardMappingQuery(request),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdSchoolStandardMappingQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateSchoolStandardMappingCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.SchoolStandardMappingId = id;

        var result = await sender.Send(request, cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteSchoolStandardMappingCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}