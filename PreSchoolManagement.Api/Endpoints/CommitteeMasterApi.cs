using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class CommitteeMasterApi
{
    public static IEndpointRouteBuilder MapCommitteeMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/CommitteeMaster")
                        .WithTags("Committee Master");

        group.MapGet("/{filter:bool}", GetAll)
            .WithName("GetAllCommittee")
            .WithSummary("Get all committee master records.")
            .WithDescription("Return all committee master record")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetCommitteeById")
            .WithSummary("Get committee by Id")
            .WithDescription("Returns a committee record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateCommittee")
            .WithSummary("Create Committee")
            .WithDescription("Creates a new committee master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}",Update)
            .WithName("UpdateCommittee")
            .WithSummary("Update Committee ")
            .WithDescription("Updates an existing committee master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteCommittee")
            .WithSummary("Delete Committee")
            .WithDescription("Deletes a committee master record.")
            .RequireAuthorization();
        return app;
    }

    private static async Task<IResult>GetAll(bool filter,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllCommitteeMasterQuery(filter),cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(ISender sender,CancellationToken cancellationToken,int id)
    {
        var result = await sender.Send(new GetByIdCommitteeMasterQuery(id),
        cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(CreateCommitteeMasterCommand command,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(command,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(int id,UpdateCommitteeMasterCommand request,ISender sender,CancellationToken cancellationToken)
    {
        request.CommitteeId = id;
        var result = await sender.Send(request,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(int id, ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteCommitteeMasterCommand(id),cancellationToken);
        return TypedResults.Ok(result);
    }
}