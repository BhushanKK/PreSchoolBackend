using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllCasteMasterHandler(ICasteMasterService service) 
    : IRequestHandler<GetAllCasteMasterQuery, ApiResponse<List<CasteMaster>>>
{
    public async Task<ApiResponse<List<CasteMaster>>> Handle(GetAllCasteMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(cancellationToken);
        
        if(data!=null)
        {
            return ApiResponse<List<CasteMaster>>.SuccessResponse
            (
                data, 
                MessageHelper.Retrieved(EntityDescription.Caste.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<CasteMaster>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Caste.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
