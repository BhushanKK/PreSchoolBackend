using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class MediumMasterApi
{
    public static IEndpointRouteBuilder MapMediumMasterEndpoints(this IEndpointRouteBuilder app)
    {
         var group = app.MapGroup("/api/MediumMaster")
                       .WithTags("Medium Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllMedium")
            .WithSummary("Get all Medium masters")
            .WithDescription("Returns all Medium master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetMediumById")
            .WithSummary("Get Medium by Id")
            .WithDescription("Returns a Medium master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateMedium")
            .WithSummary("Create Medium")
            .WithDescription("Creates a new Medium master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateMedium")
            .WithSummary("Update Medium")
            .WithDescription("Updates an existing Medium master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteMedium")
            .WithSummary("Delete Medium")
            .WithDescription("Deletes a Medium master record.")
            .RequireAuthorization();

        return app;
    }


    private static async Task<IResult> GetAll (ISender sender ,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllMediumMasterQuery(),cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById (int id,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetByIdMediumMasterQuery(id),cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create (CreateMediumMasterCommand command,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(command,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update (int id, UpdateMediumMasterCommand request,ISender sender,CancellationToken cancellationToken)
    {
        request.MediumId = id;
        var result = await sender.Send(request,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete (int id,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteMediumMasterCommand(id),cancellationToken);
        return TypedResults.Ok(result);
    }
}