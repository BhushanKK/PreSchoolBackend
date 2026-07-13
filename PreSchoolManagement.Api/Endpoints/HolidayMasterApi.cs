using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class HolidayMasterApi
{
    public static IEndpointRouteBuilder MapHolidayMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/HolidayMaster")
                       .WithTags("Holiday Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllHolidays")
            .WithSummary("Get all Holiday masters")
            .WithDescription("Returns all Holiday master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetHolidayById")
            .WithSummary("Get Holiday by Id")
            .WithDescription("Returns a Holiday master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateHoliday")
            .WithSummary("Create Holiday")
            .WithDescription("Creates a new Holiday master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateHoliday")
            .WithSummary("Update Holiday")
            .WithDescription("Updates an existing Holiday master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteHoliday")
            .WithSummary("Delete Holiday")
            .WithDescription("Deletes a Holiday master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllHolidayMasterQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdHolidayMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateHolidayMasterCommand command,
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
        UpdateHolidayMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.HolidayId = id;

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
            new DeleteHolidayMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}