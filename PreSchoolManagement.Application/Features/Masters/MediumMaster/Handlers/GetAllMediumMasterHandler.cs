using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;
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
    :IRequestHandler<GetAllMediumMasterQuery, ApiResponse<PaginatedResult<MediumMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<MediumMaster>>> Handle(GetAllMediumMasterQuery request,CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString());

        var mediums = await service.GetAllAsync(request.Request, cancellationToken);
        return ApiResponse<PaginatedResult<MediumMaster>>.SuccessResponse
        (
            mediums,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}