using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class BoardMasterApi
{
    public static IEndpointRouteBuilder MapBoardMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/BoardMaster")
                       .WithTags("Board Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllBoards")
            .WithSummary("Get all Board masters")
            .WithDescription("Returns all Board master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetBoardById")
            .WithSummary("Get Board by Id")
            .WithDescription("Returns a Board master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateBoard")
            .WithSummary("Create Board")
            .WithDescription("Creates a new Board master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateBoard")
            .WithSummary("Update Board")
            .WithDescription("Updates an existing Board master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteBoard")
            .WithSummary("Delete Board")
            .WithDescription("Deletes a Board master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> Create(CreateBoardMasterCommand command,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(command,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetAll(ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllBoardMasterQuery(),cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(int id,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetByIdBoardMasterQuery(id),cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(int id,UpdateBoardMasterCommand request,ISender sender,CancellationToken cancellationToken)
    {
        request.BoardId = id;
        var result = await sender.Send(request,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult>Delete(int id,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteBoardMasterCommand(id),cancellationToken);
        return TypedResults.Ok(result);
    }
}