using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Api.Endpoints;

public static class ChairmanMasterApi
{
    public static IEndpointRouteBuilder MapChairmanMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/Chairmanmaster")
                       .WithTags("Chairman Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllChairmans")
            .WithSummary("Get all Chairman masters")
            .WithDescription("Returns paginated Chairman master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/dropdown", GetAllActiveChairmans)
            .WithName("GetAllActiveChairmans")
            .WithSummary("Get all active Chairman for dropdowns.")
            .WithDescription("Returns paginated active Chairman.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetChairmanById")
            .WithSummary("Get Chairman by Id")
            .WithDescription("Returns a Chairman master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateChairman")
            .WithSummary("Create Chairman")
            .WithDescription("Creates a new Chairman master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateChairman")
            .WithSummary("Update Chairman")
            .WithDescription("Updates an existing Chairman master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteChairman")
            .WithSummary("Delete Chairman")
            .WithDescription("Deletes a Chairman master record.")
            .RequireAuthorization();

        return app;
    }

     private static async Task<IResult> GetAll(
        [AsParameters] PaginationRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllChairmanMasterQuery(request),
            cancellationToken);

        return TypedResults.Ok(result);
    }
    private static async Task<IResult> GetAllActiveChairmans(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetChairmanDropdownQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }
    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdChairmanMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateChairmanMasterCommand command,
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
        UpdateChairmanMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.ChairmanId = id;
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
            new DeleteChairmanMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}