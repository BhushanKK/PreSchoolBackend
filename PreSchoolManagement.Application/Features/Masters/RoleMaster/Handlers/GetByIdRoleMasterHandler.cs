using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdRoleMasterHandler(IRoleMasterService service)
    : IRequestHandler<GetByIdRoleMasterQuery, ApiResponse<RoleMaster?>>
{
    public async Task<ApiResponse<RoleMaster?>> Handle(
        GetByIdRoleMasterQuery request,
        CancellationToken cancellationToken)
    {
        var role = await service.GetByIdAsync(request.RoleID, cancellationToken);

        if (role is null)
        {
            return ApiResponse<RoleMaster?>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Role.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<RoleMaster?>.SuccessResponse(
            role,
            MessageHelper.Retrieved(EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK);
    }
}