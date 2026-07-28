using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdChairmanMasterHandler(
    IChairmanMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization) 
    : IRequestHandler<GetByIdChairmanMasterQuery, ApiResponse<ChairmanMaster?>>
{
    public async Task<ApiResponse<ChairmanMaster?>> Handle(GetByIdChairmanMasterQuery request, CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString() ,EntityDescription.Chairman.ToString());
        
        if (request.ChairmanId <= 0)
        {
            return ApiResponse<ChairmanMaster?>.FailureResponse
            (
                messageHelper.InvalidIdEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Chairman.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.ChairmanId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<ChairmanMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Chairman.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<ChairmanMaster?>.SuccessResponse
        (
            data, 
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Chairman.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}
