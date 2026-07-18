using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class DistrictMasterApi
{
    public static IEndpointRouteBuilder MapDistrictMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/districtmaster")
                        .WithTags("District Master");
        
        group.MapGet("/", GetAll)
            .WithName("GetAllDistricts")
            .WithSummary("Get all district masters")
            .WithDescription("Returns all district master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError).RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetDistrictById")
            .WithSummary("Get all District masters")
            .WithDescription("Returns all district master record by Id.")
            .RequireAuthorization();

        group.MapPost("/",Create)
            .WithName("CreateDistrict")
            .WithSummary("Create District")
            .WithDescription("Creates a new district master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}",Update)
            .WithName("UpdateDistrict")
            .WithSummary("Update District")
            .WithDescription("Update an existing  district master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}",Delete)
            .WithName("DeleteDistrict")
            .WithSummary("Delete a district master record.")
            .RequireAuthorization();

        return app;  
    }

    private static async Task<IResult> Update(int id,
    UpdateDistrictMasterCommand request,
    ISender sender,CancellationToken cancellationToken)
    {
        request.DistrictId = id;
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
            new DeleteDistrictMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult>Create(
        CreateDistrictMasterCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            command,
            cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdDistrictMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetAll(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllDistrictMasterQuery(),cancellationToken);
        
        return TypedResults.Ok(result);
    }
}