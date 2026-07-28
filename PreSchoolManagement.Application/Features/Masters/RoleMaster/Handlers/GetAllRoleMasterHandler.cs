using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllRoleMasterHandler(
    IRoleMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllRoleMasterQuery, ApiResponse<PaginatedResult<RoleMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<RoleMaster>>> Handle(
        GetAllRoleMasterQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await service.GetAllAsync(request.Request,cancellationToken);

        return ApiResponse<PaginatedResult<RoleMaster>>.SuccessResponse
        (
            roles,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}