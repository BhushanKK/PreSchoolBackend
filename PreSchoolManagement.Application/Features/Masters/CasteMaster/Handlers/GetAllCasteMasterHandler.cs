using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllCasteMasterHandler(
    ICasteMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization) 
    : IRequestHandler<GetAllCasteMasterQuery, ApiResponse<List<CasteMasterQueryDto>>>
{
    public async Task<ApiResponse<List<CasteMasterQueryDto>>> Handle(GetAllCasteMasterQuery request, CancellationToken cancellationToken)
    {
        localization.Get("Masters" ,EntityDescription.Caste.ToString());

        var data = await service.GetAllAsync(request.filter, cancellationToken);
        
        if(data!=null)
        {
            return ApiResponse<List<CasteMasterQueryDto>>.SuccessResponse
            (
                data, 
                messageHelper.RetrievedEntity("Masters" ,EntityDescription.Caste.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<CasteMasterQueryDto>>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters" ,EntityDescription.Caste.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
