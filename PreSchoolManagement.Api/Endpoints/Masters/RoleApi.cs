using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Api.Endpoints;

public static class RoleMasterApi
{
    public static IEndpointRouteBuilder MapRoleMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/RoleMaster")
                       .WithTags("Role Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllRoles")
            .WithSummary("Get all Role masters")
            .WithDescription("Returns all Role master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/Dropdown", GetAllActiveRoles)
            .WithName("GetAllActiveRoles")
            .WithSummary("Get all active Roles for Dropdown")
            .WithDescription("Returns all active Role master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetRoleById")
            .WithSummary("Get Role by Id")
            .WithDescription("Returns a Role master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateRole")
            .WithSummary("Create Role")
            .WithDescription("Creates a new Role master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateRole")
            .WithSummary("Update Role")
            .WithDescription("Updates an existing Role master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteRole")
            .WithSummary("Delete Role")
            .WithDescription("Deletes a Role master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(
        [AsParameters] PaginationRequest request, 
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllRoleMasterQuery(request),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetAllActiveRoles(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetRoleDropdownQuery(),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdRoleMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
    
    private static async Task<IResult> Create(
        CreateRoleMasterCommand command,
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
        UpdateRoleMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.RoleId = id;

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
            new DeleteRoleMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}