using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdFinancialYearMasterHandler(IFinancialYearMasterService service) : IRequestHandler<GetByIdFinancialYearMasterQuery, ApiResponse<FinancialYearMaster?>>
{
    public async Task<ApiResponse<FinancialYearMaster?>> Handle(GetByIdFinancialYearMasterQuery request, CancellationToken cancellationToken)
    {
        if (request.FinancialYearId <= 0)
        {
            return ApiResponse<FinancialYearMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.FinancialYearId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<FinancialYearMaster?>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<FinancialYearMaster?>.SuccessResponse
        (
            data, 
            MessageHelper.Retrieved(EntityDescription.FinancialYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
