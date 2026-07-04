using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;

namespace PreSchoolManagement.Api.Endpoints;

public static class CasteMasterApi
{
    public static IEndpointRouteBuilder MapCasteMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/castemaster")
                       .WithTags("Caste Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllCastes")
            .WithSummary("Get all caste masters")
            .WithDescription("Returns all caste master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError).RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetCasteById")
            .WithSummary("Get caste by Id")
            .WithDescription("Returns a caste master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateCaste")
            .WithSummary("Create caste")
            .WithDescription("Creates a new caste master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateCaste")
            .WithSummary("Update caste")
            .WithDescription("Updates an existing caste master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteCaste")
            .WithSummary("Delete caste")
            .WithDescription("Deletes a caste master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllCasteMasterQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdCasteMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateCasteMasterCommand command,
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
        UpdateCasteMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.CasteId = id;
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
            new DeleteCasteMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}