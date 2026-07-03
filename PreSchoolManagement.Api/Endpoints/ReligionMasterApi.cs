using MediatR;
using SchoolAdmission.Application.Features.Commands;
using SchoolAdmission.Application.Features.Queries;
using SchoolAdmission.Domain.Dtos;
namespace SchoolAdmission.Api.Endpoints;

public static class ReligionMasterApi
{
    public static IEndpointRouteBuilder MapReligionMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/Religionmaster")
                       .WithTags("Religion Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllReligions")
            .WithSummary("Get all Religion masters")
            .WithDescription("Returns all Religion master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:int}", GetById)
            .WithName("GetReligionById")
            .WithSummary("Get Religion by Id")
            .WithDescription("Returns a Religion master record by Id.");

        group.MapPost("/", Create)
            .WithName("CreateReligion")
            .WithSummary("Create Religion")
            .WithDescription("Creates a new Religion master record.");

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateReligion")
            .WithSummary("Update Religion")
            .WithDescription("Updates an existing Religion master record.");

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteReligion")
            .WithSummary("Delete Religion")
            .WithDescription("Deletes a Religion master record.");

        return app;
    }

    private static async Task<IResult> GetAll(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllReligionMasterQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdReligionMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateReligionMasterCommand command,
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
        ReligionMasterCommandDto request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateReligionMasterCommand
        {
            ReligionId = id,
            IsMinority = request.IsMinority,
            Religion = request.Religion
        };

        var result = await sender.Send(
            command,
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteReligionMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}