using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllMediumMasterHandler(
    IMediumMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    :IRequestHandler<GetAllMediumMasterQuery, ApiResponse<List<MediumMaster>>>
{
    public async Task<ApiResponse<List<MediumMaster>>> Handle(GetAllMediumMasterQuery request,CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Medium.ToString());

        var mediums = await service.GetAllAsync(cancellationToken);
        return ApiResponse<List<MediumMaster>>.SuccessResponse
        (
            mediums,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Medium.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}