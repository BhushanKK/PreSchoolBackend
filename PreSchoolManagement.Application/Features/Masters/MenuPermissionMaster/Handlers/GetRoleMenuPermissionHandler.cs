using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetRoleMenuPermissionHandler(
    IRoleMenuPermissionService service)
    : IRequestHandler<GetRoleMenuPermissionQuery,
        ApiResponse<List<RoleMenuPermissionDto>>>
{
    public async Task<ApiResponse<List<RoleMenuPermissionDto>>> Handle(
        GetRoleMenuPermissionQuery request,
        CancellationToken cancellationToken)
    {
        var permissions = await service.GetByRoleAsync(
            request.RoleId,
            cancellationToken);

        return ApiResponse<List<RoleMenuPermissionDto>>
            .SuccessResponse(
                permissions,
                "Role menu permissions retrieved successfully.",
                (int)HttpStatusCode.OK);
    }
}