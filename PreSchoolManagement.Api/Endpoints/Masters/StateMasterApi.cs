using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Api.Endpoints;

public static class StateMasterApi
{
    public static IEndpointRouteBuilder MapStateMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/statemaster")
                        .WithTags("State Master");

        group.MapGet("/", GetAll)
              .WithName("GetAllState")
              .WithSummary("Get all state masters")
              .WithDescription("Return all state master records.")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status500InternalServerError)
              .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetStateById")
            .WithSummary("Get state by Id")
            .WithDescription("Returns a state master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateState")
            .WithSummary("Create State")
            .WithDescription("Creates a new state master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}",Update)
            .WithName("UpdateState")
            .WithSummary("Update State")
            .WithDescription("Updates an existing state master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteState")
            .WithSummary("Delete State")
            .WithDescription("Deletes a state master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(bool filter,
    [AsParameters] PaginationRequest request, 
    ISender sender,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllStateMasterQuery(request),cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult>GetById
    ( int id, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetByIdStateMasterQuery(id),
        cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateStateMasterCommand command, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(
        int id,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteStateMasterCommand(id),cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(int id, 
    UpdateStateMasterCommand request,
    ISender sender,CancellationToken cancellationToken)
    {
        request.StateId = id;
        var result = await sender.Send(request,cancellationToken);

        return TypedResults.Ok(result);
    }
    
}