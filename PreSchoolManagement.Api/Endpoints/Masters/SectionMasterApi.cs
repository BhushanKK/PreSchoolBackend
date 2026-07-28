using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Api.Endpoints;

public static class SectionMasterApi
{
    public static IEndpointRouteBuilder MapSectionMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/SectionMaster")
                       .WithTags("Section Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllSections")
            .WithSummary("Get all Section masters")
            .WithDescription("Returns all Section master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetSectionById")
            .WithSummary("Get Section by Id")
            .WithDescription("Returns a Section master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateSection")
            .WithSummary("Create Section")
            .WithDescription("Creates a new Section master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateSection")
            .WithSummary("Update Section")
            .WithDescription("Updates an existing Section master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteSection")
            .WithSummary("Delete Section")
            .WithDescription("Deletes a Section master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        [AsParameters] PaginationRequest request, 
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllSectionMasterQuery(request),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdSectionMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateSectionMasterCommand command,
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
        UpdateSectionMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.SectionId = id;

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
            new DeleteSectionMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}