using System.Net;
using MediatR;
using PreSchoolManagement.Shared.Utils;
using SchoolAdmission.Application.Features.Queries;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;


public class GetAllRelligionMasterHandler(IReligionMasterService service) : IRequestHandler<GetAllReligionMasterQuery, ApiResponse<List<ReligionMaster>>>
{
    public async Task<ApiResponse<List<ReligionMaster>>> Handle(GetAllReligionMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(cancellationToken);
        if(data!=null)
        {
            return ApiResponse<List<ReligionMaster>>.SuccessResponse
            (
                data, 
                MessageHelper.Retrieved(EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<ReligionMaster>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
