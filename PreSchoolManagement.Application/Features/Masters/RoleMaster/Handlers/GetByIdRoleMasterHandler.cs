using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdRoleMasterHandler(IRoleMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetByIdRoleMasterQuery, ApiResponse<RoleMaster?>>
{
    public async Task<ApiResponse<RoleMaster?>> Handle(
        GetByIdRoleMasterQuery request,
        CancellationToken cancellationToken)
    {
        var role = await service.GetByIdAsync(request.RoleID, cancellationToken);

        if (role is null)
        {
            return ApiResponse<RoleMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Role.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<RoleMaster?>.SuccessResponse
        (
            role,
            messageHelper.RetrievedEntity("Masters", EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}