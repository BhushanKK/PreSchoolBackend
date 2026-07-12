using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Domain.Dtos;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllCasteMasterHandler(ICasteMasterService service) 
    : IRequestHandler<GetAllCasteMasterQuery, ApiResponse<List<CasteMasterQueryDto>>>
{
    public async Task<ApiResponse<List<CasteMasterQueryDto>>> Handle(GetAllCasteMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.filter, cancellationToken);
        
        if(data!=null)
        {
            return ApiResponse<List<CasteMasterQueryDto>>.SuccessResponse
            (
                data, 
                MessageHelper.Retrieved(EntityDescription.Caste.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<CasteMasterQueryDto>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Caste.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
