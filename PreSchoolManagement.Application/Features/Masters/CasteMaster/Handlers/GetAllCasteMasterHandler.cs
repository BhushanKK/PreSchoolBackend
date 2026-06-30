using System.Net;
using MediatR;
using SchoolAdmission.Application.Features.CasteMasters.Queries;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Handlers;

public class GetAllCasteMasterHandler(ICasteMasterService service) : IRequestHandler<GetAllCasteMasterQuery, ApiResponse<List<CasteMaster>>>
{
    private readonly ICasteMasterService _service = service;

    public async Task<ApiResponse<List<CasteMaster>>> Handle(GetAllCasteMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await _service.GetAllAsync(cancellationToken);
        return ApiResponse<List<CasteMaster>>.SuccessResponse(data, "Caste masters retrieved successfully.", (int)HttpStatusCode.OK);
    }
}
