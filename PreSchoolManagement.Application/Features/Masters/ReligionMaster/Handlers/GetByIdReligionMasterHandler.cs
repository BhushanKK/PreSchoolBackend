using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Infrastructure.Interfaces;
namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdReligionMasterHandler(IReligionMasterService service) 
    : IRequestHandler<GetByIdReligionMasterQuery, ApiResponse<ReligionMaster?>>
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