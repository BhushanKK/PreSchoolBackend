using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class DivisionMasterApi
{
    public static IEndpointRouteBuilder MapDivisionMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/DivisionMaster")
                       .WithTags("Division Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllDivisions")
            .WithSummary("Get all Division masters")
            .WithDescription("Returns all Division master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetDivisionById")
            .WithSummary("Get Division by Id")
            .WithDescription("Returns a Division master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateDivision")
            .WithSummary("Create Division")
            .WithDescription("Creates a new Division master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateDivision")
            .WithSummary("Update Division")
            .WithDescription("Updates an existing Division master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteDivision")
            .WithSummary("Delete Division")
            .WithDescription("Deletes a Division master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllDivisionMasterQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdDivisionMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateDivisionMasterCommand command,
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
        UpdateDivisionMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.DivisionId = id;

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
            new DeleteDivisionMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}