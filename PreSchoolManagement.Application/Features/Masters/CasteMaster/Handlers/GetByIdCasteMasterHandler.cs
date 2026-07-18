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

public class GetByIdCasteMasterHandler(
    ICasteMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization) 
    : IRequestHandler<GetByIdCasteMasterQuery, ApiResponse<CasteMaster?>>
{
    public async Task<ApiResponse<CasteMaster?>> Handle(GetByIdCasteMasterQuery request, CancellationToken cancellationToken)
    {
        localization.Get("Masters" ,EntityDescription.Caste.ToString());
        
        if (request.CasteId <= 0)
        {
            return ApiResponse<CasteMaster?>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters" ,EntityDescription.Caste.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.CasteId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<CasteMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters" ,EntityDescription.Caste.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<CasteMaster?>.SuccessResponse
        (
            data, 
            messageHelper.RetrievedEntity("Masters" ,EntityDescription.Caste.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}
