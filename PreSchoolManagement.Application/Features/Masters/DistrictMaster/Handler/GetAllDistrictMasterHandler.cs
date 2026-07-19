using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllDistrictMasterHandler(
    IDistrictMasterService service,
    IMessageHelper messageHelper)
    :IRequestHandler<GetAllDistrictMasterQuery, ApiResponse<List<DistrictMasterQueryDto>>>
{
    public async Task<ApiResponse<List<DistrictMasterQueryDto>>> Handle(GetAllDistrictMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(cancellationToken);
        
        if(data!=null)
        {
            return ApiResponse<List<DistrictMasterQueryDto>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity("Masters",EntityDescription.District.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<DistrictMasterQueryDto>>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.District.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}