using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdDivisionMasterHandler(IDivisionMasterService service)
    : IRequestHandler<GetByIdDivisionMasterQuery, ApiResponse<DivisionMaster?>>
{
    public async Task<ApiResponse<DivisionMaster?>> Handle(
        GetByIdDivisionMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Division = await service.GetByIdAsync(request.DivisionId, cancellationToken);

        if (Division is null)
        {
            return ApiResponse<DivisionMaster?>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Division.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<DivisionMaster?>.SuccessResponse(
            Division,
            MessageHelper.Retrieved(EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK);
    }
}