using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class SaveRoleMenuPermissionHandler(
    IRoleMenuPermissionService service,
    ICurrentUserService currentUser)
    : IRequestHandler<SaveRoleMenuPermissionCommand,
        ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(
        SaveRoleMenuPermissionCommand request,
        CancellationToken cancellationToken)
    {
        await service.SaveAsync(
            request,
            currentUser.UserId,
            cancellationToken);

        return ApiResponse<bool>.SuccessResponse(
            true,
            "Role menu permissions saved successfully.",
            (int)HttpStatusCode.OK);
    }
}