using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class RoleMenuPermissionApi
{
    public static IEndpointRouteBuilder MapRoleMenuPermissionEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/RoleMenuPermission")
            .WithTags("Role Menu Permission");

        group.MapGet("/{roleId:int}", GetByRole)
            .WithName("GetRoleMenuPermission")
            .WithSummary("Get Role Menu Permission")
            .WithDescription("Returns all menu permissions for the selected role.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapPost("/", Save)
            .WithName("SaveRoleMenuPermission")
            .WithSummary("Save Role Menu Permission")
            .WithDescription("Creates or updates role menu permissions.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetByRole(
        int roleId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetRoleMenuPermissionQuery(roleId),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Save(
        SaveRoleMenuPermissionCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            command,
            cancellationToken);

        return TypedResults.Ok(result);
    }
}