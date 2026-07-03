using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;

using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolAdmission.Application.Features.Queries;
namespace SchoolAdmission.Application.Features.Handlers;

public class GetByIdReligionMasterHandler(IReligionMasterService service) : IRequestHandler<GetByIdReligionMasterQuery, ApiResponse<ReligionMaster?>>
{
    public async Task<ApiResponse<ReligionMaster?>> Handle(GetByIdReligionMasterQuery request, CancellationToken cancellationToken)
    {
        if (request.ReligionId <= 0)
        {
            return ApiResponse<ReligionMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.Religion.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.ReligionId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<ReligionMaster?>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Religion.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<ReligionMaster?>.SuccessResponse
        (
            data, 
            MessageHelper.Retrieved(EntityDescription.Religion.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}