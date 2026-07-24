using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllDistrictMasterHandler(
    IDistrictMasterService service,
    IMessageHelper messageHelper)
    :IRequestHandler<GetAllDistrictMasterQuery, ApiResponse<PaginatedResult<DistrictMasterQueryDto>>>
{
    public async Task<ApiResponse<PaginatedResult<DistrictMasterQueryDto>>> 
    Handle(GetAllDistrictMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.Request,cancellationToken);
        
        if(data!=null)
        {
            return ApiResponse<PaginatedResult<DistrictMasterQueryDto>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.District.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<PaginatedResult<DistrictMasterQueryDto>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.District.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}