using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllStandardMasterHandler(
    IStandardMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllStandardMasterQuery, ApiResponse<List<StandardMaster>>>
{
    public async Task<ApiResponse<List<StandardMaster>>> Handle(
        GetAllStandardMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Standard.ToString());
        
        var Standards = await service.GetAllAsync(request.filter,cancellationToken);

        return ApiResponse<List<StandardMaster>>.SuccessResponse
        (
            Standards,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}