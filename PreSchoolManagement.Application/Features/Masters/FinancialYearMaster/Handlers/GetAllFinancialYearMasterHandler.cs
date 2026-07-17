using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllFinancialYearMasterHandler(
    IFinancialYearMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllFinancialYearMasterQuery, ApiResponse<List<FinancialYearMaster>>>
{
    public async Task<ApiResponse<List<FinancialYearMaster>>> Handle(GetAllFinancialYearMasterQuery request, CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.FinancialYear.ToString());

        var data = await service.GetAllAsync(request.filter, cancellationToken);

        if (data != null)
        {
            return ApiResponse<List<FinancialYearMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity("Masters", EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<FinancialYearMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
