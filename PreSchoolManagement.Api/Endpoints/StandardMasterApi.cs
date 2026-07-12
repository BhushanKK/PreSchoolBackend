using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class StandardMasterApi
{
    public static IEndpointRouteBuilder MapStandardMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/StandardMaster")
                       .WithTags("Standard Master");

        group.MapGet("/{filter:bool}", GetAll)
            .WithName("GetAllStandards")
            .WithSummary("Get all Standard masters")
            .WithDescription("Returns all Standard master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetStandardById")
            .WithSummary("Get Standard by Id")
            .WithDescription("Returns a Standard master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateStandard")
            .WithSummary("Create Standard")
            .WithDescription("Creates a new Standard master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateStandard")
            .WithSummary("Update Standard")
            .WithDescription("Updates an existing Standard master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteStandard")
            .WithSummary("Delete Standard")
            .WithDescription("Deletes a Standard master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(bool filter,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllStandardMasterQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdStandardMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateStandardMasterCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            command,
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(
        int id,
        UpdateStandardMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.StandardId = id;

        var result = await sender.Send(
            request,
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteStandardMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}