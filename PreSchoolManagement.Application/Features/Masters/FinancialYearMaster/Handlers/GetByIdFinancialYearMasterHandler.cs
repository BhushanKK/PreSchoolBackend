using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdFinancialYearMasterHandler(
    IFinancialYearMasterService service,
    IMessageHelper messageHelper) 
    : IRequestHandler<GetByIdFinancialYearMasterQuery, ApiResponse<FinancialYearMaster?>>
{
    public async Task<ApiResponse<FinancialYearMaster?>> Handle(GetByIdFinancialYearMasterQuery request, CancellationToken cancellationToken)
    {
        if (request.FinancialYearId <= 0)
        {
            return ApiResponse<FinancialYearMaster?>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.FinancialYearId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<FinancialYearMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<FinancialYearMaster?>.SuccessResponse
        (
            data, 
            messageHelper.RetrievedEntity("Masters",EntityDescription.FinancialYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
