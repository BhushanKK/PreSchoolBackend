using System.Net;
using MediatR;
using PreSchoolManagement.Shared.Utils;
using SchoolAdmission.Application.Features.CasteMasters.Queries;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Handlers;

public class GetAllCasteMasterHandler(ICasteMasterService service) : IRequestHandler<GetAllCasteMasterQuery, ApiResponse<List<CasteMaster>>>
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
