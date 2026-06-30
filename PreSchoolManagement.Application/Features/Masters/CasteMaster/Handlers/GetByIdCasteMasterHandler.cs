using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using SchoolAdmission.Application.Features.CasteMasters.Queries;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Handlers;

public class GetByIdCasteMasterHandler(ICasteMasterService service) : IRequestHandler<GetByIdCasteMasterQuery, ApiResponse<CasteMaster?>>
{
    public async Task<ApiResponse<CasteMaster?>> Handle(GetByIdCasteMasterQuery request, CancellationToken cancellationToken)
    {
        if (request.CasteId <= 0)
        {
            return ApiResponse<CasteMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.Caste.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.CasteId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<CasteMaster?>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Caste.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<CasteMaster?>.SuccessResponse
        (
            data, 
            MessageHelper.Retrieved(EntityDescription.Caste.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
