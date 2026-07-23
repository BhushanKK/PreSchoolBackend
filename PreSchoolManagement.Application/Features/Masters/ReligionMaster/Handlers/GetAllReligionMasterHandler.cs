using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

public class GetAllRelligionMasterHandler(
    IReligionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllReligionMasterQuery, ApiResponse<List<ReligionMaster>>>
{

    public async Task<ApiResponse<List<ReligionMaster>>> Handle(GetAllReligionMasterQuery request, CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString());

        var data = await service.GetAllAsync(request.filter, cancellationToken);
        if (data != null)
        {
            return ApiResponse<List<ReligionMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<ReligionMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
