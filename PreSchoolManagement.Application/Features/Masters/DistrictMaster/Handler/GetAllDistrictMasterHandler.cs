using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllDistrictMasterHandler(IDistrictMasterService service)
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
                MessageHelper.Retrieved(EntityDescription.District.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<DistrictMasterQueryDto>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.District.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}