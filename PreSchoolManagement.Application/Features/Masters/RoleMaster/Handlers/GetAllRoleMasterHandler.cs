using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllRoleMasterHandler(IRoleMasterService service)
    : IRequestHandler<GetAllRoleMasterQuery, ApiResponse<List<RoleMaster>>>
{
    public async Task<ApiResponse<List<RoleMaster>>> Handle(
        GetAllRoleMasterQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await service.GetAllAsync(cancellationToken);

        return ApiResponse<List<RoleMaster>>.SuccessResponse(
            roles,
            MessageHelper.Retrieved(EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK);
    }
}