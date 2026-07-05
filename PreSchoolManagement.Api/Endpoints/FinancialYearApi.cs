using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class FinancialYearMasterApi
{
    public static IEndpointRouteBuilder MapFinancialYearMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/FinancialYearmaster")
                       .WithTags("FinancialYear Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllFinancialYears")
            .WithSummary("Get all FinancialYear masters")
            .WithDescription("Returns all FinancialYear master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError).RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetFinancialYearById")
            .WithSummary("Get FinancialYear by Id")
            .WithDescription("Returns a FinancialYear master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateFinancialYear")
            .WithSummary("Create FinancialYear")
            .WithDescription("Creates a new FinancialYear master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateFinancialYear")
            .WithSummary("Update FinancialYear")
            .WithDescription("Updates an existing FinancialYear master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteFinancialYear")
            .WithSummary("Delete FinancialYear")
            .WithDescription("Deletes a FinancialYear master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllFinancialYearMasterQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdFinancialYearMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateFinancialYearMasterCommand command,
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
        UpdateFinancialYearMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.FinancialYearId = id;
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
            new DeleteFinancialYearMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}