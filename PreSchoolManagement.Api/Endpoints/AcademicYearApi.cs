using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;

namespace PreSchoolManagement.Api.Endpoints;

public static class AcademicYearMasterApi
{
    public static IEndpointRouteBuilder MapAcademicYearMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/AcademicYearmaster")
                       .WithTags("AcademicYear Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllAcademicYears")
            .WithSummary("Get all AcademicYear masters")
            .WithDescription("Returns all AcademicYear master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError).RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetAcademicYearById")
            .WithSummary("Get AcademicYear by Id")
            .WithDescription("Returns a AcademicYear master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateAcademicYear")
            .WithSummary("Create AcademicYear")
            .WithDescription("Creates a new AcademicYear master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateAcademicYear")
            .WithSummary("Update AcademicYear")
            .WithDescription("Updates an existing AcademicYear master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteAcademicYear")
            .WithSummary("Delete AcademicYear")
            .WithDescription("Deletes a AcademicYear master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllAcademicYearMasterQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdAcademicYearMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateAcademicYearMasterCommand command,
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
        AcademicYearMasterDto request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAcademicYearMasterCommand
        {
            AcademicYearId = id,
            AcademicYearName = request.AcademicYearName
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
            new DeleteAcademicYearMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}