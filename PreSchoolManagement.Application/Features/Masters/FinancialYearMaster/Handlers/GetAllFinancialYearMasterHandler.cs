using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllFinancialYearMasterHandler(
    IFinancialYearMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllFinancialYearMasterQuery, ApiResponse<PaginatedResult<FinancialYearMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<FinancialYearMaster>>> Handle(GetAllFinancialYearMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.Request, cancellationToken);

        if (data != null)
        {
            return ApiResponse<PaginatedResult<FinancialYearMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<PaginatedResult<FinancialYearMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(), EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
