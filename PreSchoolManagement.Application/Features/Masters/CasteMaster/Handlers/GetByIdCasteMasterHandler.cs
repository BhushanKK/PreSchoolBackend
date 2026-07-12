using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdCasteMasterHandler(ICasteMasterService service) 
    : IRequestHandler<GetByIdCasteMasterQuery, ApiResponse<CasteMaster?>>
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
