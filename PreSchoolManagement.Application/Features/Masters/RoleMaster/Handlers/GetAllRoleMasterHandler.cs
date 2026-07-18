using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllRoleMasterHandler(
    IRoleMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllRoleMasterQuery, ApiResponse<List<RoleMaster>>>
{
    public async Task<ApiResponse<List<RoleMaster>>> Handle(
        GetAllRoleMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Role.ToString());

        var roles = await service.GetAllAsync(cancellationToken);

        return ApiResponse<List<RoleMaster>>.SuccessResponse
        (
            roles,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}