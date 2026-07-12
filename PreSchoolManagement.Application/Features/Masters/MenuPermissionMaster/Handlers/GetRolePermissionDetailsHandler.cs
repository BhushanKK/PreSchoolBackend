using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetRolePermissionDetailsHandler(
    IPermissionService permissionService)
    : IRequestHandler<GetRolePermissionDetailsQuery, ApiResponse<List<UserPermissionDto>>>
{
    public async Task<ApiResponse<List<UserPermissionDto>>> Handle(
        GetRolePermissionDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var permissions = await permissionService
            .GetUserPermissionsAsync(
                request.roleId,
                cancellationToken);

        if (permissions is null || !permissions.Any())
        {
            return ApiResponse<List<UserPermissionDto>>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.RolePermission.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<List<UserPermissionDto>>.SuccessResponse(
            permissions,
            MessageHelper.Retrieved(EntityDescription.RolePermission.ToString()),
            (int)HttpStatusCode.OK);
    }
}