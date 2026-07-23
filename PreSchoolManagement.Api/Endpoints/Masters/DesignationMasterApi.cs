using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;

public static class DesignationMasterApi
{
    public static IEndpointRouteBuilder MapDesignationMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/DesignationMaster")
                        .WithTags("Designation Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllDesignation")
            .WithSummary("Get all Designation masters with pagination.")
            .WithDescription("Returns all Designation master records with pagination.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetDesignationById")
            .WithSummary("Get Designation by Id")
            .WithDescription("Returns a Designation master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateDesignation")
            .WithSummary("Create Designation")
            .WithDescription("Creates a new designation master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateDesignation")
            .WithSummary("Update Designation")
            .WithDescription("Updates an existing Designation master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteDesignation")
            .WithSummary("Delete Designation")
            .WithDescription("Deletes a Designation master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult>GetAll(
    [AsParameters] PaginationRequest request,
    ISender sender,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllDesignationMasterQuery(request),
            cancellationToken
        );
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(int id,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdDesignationMasterQuery(id),
            cancellationToken
        );

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateDesignationMasterCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result =await sender.Send(command,cancellationToken);

        return TypedResults.Ok(result);
    }
    
    private static async Task<IResult> Update(
        int id,
        UpdateDesignationMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.DesignationId = id ;

        var result = await sender.Send(request,cancellationToken);
        return TypedResults.Ok(result);
    }
    
    private static async Task<IResult> Delete(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteDesignationMasterCommand(id),
            cancellationToken);
        
        return TypedResults.Ok(result);
    }
}