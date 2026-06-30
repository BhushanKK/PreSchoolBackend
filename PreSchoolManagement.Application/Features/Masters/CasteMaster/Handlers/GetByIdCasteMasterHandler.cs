using System.Net;
using MediatR;
using SchoolAdmission.Application.Features.CasteMasters.Queries;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Handlers;

public class GetByIdCasteMasterHandler(ICasteMasterService service) : IRequestHandler<GetByIdCasteMasterQuery, ApiResponse<CasteMaster?>>
{
    public async Task<ApiResponse<CasteMaster?>> Handle(GetByIdCasteMasterQuery request, CancellationToken cancellationToken)
    {
        if (request.CasteId <= 0)
            return ApiResponse<CasteMaster?>.FailureResponse("Invalid caste id.", (int)HttpStatusCode.BadRequest);

        var data = await service.GetByIdAsync(request.CasteId, cancellationToken);

        if (data is null)
            return ApiResponse<CasteMaster?>.FailureResponse("Caste not found.", (int)HttpStatusCode.NotFound);

        return ApiResponse<CasteMaster?>.SuccessResponse(data, "Caste retrieved successfully.", (int)HttpStatusCode.OK);
    }
}
