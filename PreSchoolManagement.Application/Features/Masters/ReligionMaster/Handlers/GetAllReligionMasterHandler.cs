using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

public class GetAllRelligionMasterHandler(
    IReligionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllReligionMasterQuery, ApiResponse<PaginatedResult<ReligionMaster>>>
{

    public async Task<ApiResponse<PaginatedResult<ReligionMaster>>> Handle(GetAllReligionMasterQuery request, CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString());

        var data = await service.GetAllAsync(request.Request, cancellationToken);
        if (data != null)
        {
            return ApiResponse<PaginatedResult<ReligionMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<PaginatedResult<ReligionMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
