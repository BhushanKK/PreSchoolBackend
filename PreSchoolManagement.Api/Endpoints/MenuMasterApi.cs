using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class MenuMasterApi
{
    public static IEndpointRouteBuilder MapMenuMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/MenuMaster")
                       .WithTags("Menu Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllMenus")
            .WithSummary("Get all Menu masters")
            .WithDescription("Returns all Menu master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetMenuById")
            .WithSummary("Get Menu by Id")
            .WithDescription("Returns a Menu master record by Id.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateMenu")
            .WithSummary("Create Menu")
            .WithDescription("Creates a new Menu master record.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateMenu")
            .WithSummary("Update Menu")
            .WithDescription("Updates an existing Menu master record.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteMenu")
            .WithSummary("Delete Menu")
            .WithDescription("Deletes a Menu master record.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        group.MapGet("/ParentMenus", GetParentMenus)
            .WithName("GetParentMenus")
            .WithSummary("Get Parent Menus")
            .WithDescription("Returns all active parent menus for dropdown.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllMenuMasterQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetMenuMasterByIdQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateMenuMasterCommand command,
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
        UpdateMenuMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.MenuId = id;

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
            new DeleteMenuMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetParentMenus(
    ISender sender,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetParentMenuQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}