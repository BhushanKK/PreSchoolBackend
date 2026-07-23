using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllStandardMasterHandler(
    IStandardMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllStandardMasterQuery, ApiResponse<PaginatedResult<StandardMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<StandardMaster>>> Handle(
        GetAllStandardMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(),EntityDescription.Standard.ToString());
        
        var Standards = await service.GetAllAsync(request.Request,cancellationToken);

        return ApiResponse<PaginatedResult<StandardMaster>>.SuccessResponse
        (
            Standards,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}