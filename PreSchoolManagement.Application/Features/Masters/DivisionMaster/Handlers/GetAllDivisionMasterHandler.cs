using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllDivisionMasterHandler(IDivisionMasterService service)
    : IRequestHandler<GetAllDivisionMasterQuery, ApiResponse<List<DivisionMaster>>>
{
    public async Task<ApiResponse<List<DivisionMaster>>> Handle(
        GetAllDivisionMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Divisions = await service.GetAllAsync(request.filter, cancellationToken);

        return ApiResponse<List<DivisionMaster>>.SuccessResponse(
            Divisions,
            MessageHelper.Retrieved(EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK);
    }
}