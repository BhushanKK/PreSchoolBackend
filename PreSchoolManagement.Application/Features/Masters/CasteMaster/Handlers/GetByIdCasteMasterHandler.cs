using MediatR;
using SchoolAdmission.Application.Features.CasteMasters.Queries;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Handlers;

public class GetByIdCasteMasterHandler(ICasteMasterService service) : IRequestHandler<GetByIdCasteMasterQuery, ApiResponse<CasteMaster?>>
{
    private readonly ICasteMasterService _service = service;

    public async Task<ApiResponse<CasteMaster?>> Handle(GetByIdCasteMasterQuery request, CancellationToken cancellationToken)
    {
        if (request.CasteId <= 0)
            return ApiResponse<CasteMaster?>.FailureResponse("Invalid caste id.", 400);

        var data = await _service.GetByIdAsync(request.CasteId, cancellationToken);

        if (data is null)
            return ApiResponse<CasteMaster?>.FailureResponse("Caste not found.", 404);

        return ApiResponse<CasteMaster?>.SuccessResponse(data, "Caste retrieved successfully.", 200);
    }
}
