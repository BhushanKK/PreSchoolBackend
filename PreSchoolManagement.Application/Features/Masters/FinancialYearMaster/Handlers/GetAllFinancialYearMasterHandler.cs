using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllFinancialYearMasterHandler(IFinancialYearMasterService service) : IRequestHandler<GetAllFinancialYearMasterQuery, ApiResponse<List<FinancialYearMaster>>>
{
    public async Task<ApiResponse<List<FinancialYearMaster>>> Handle(GetAllFinancialYearMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(cancellationToken);
        if(data!=null)
        {
            return ApiResponse<List<FinancialYearMaster>>.SuccessResponse
            (
                data, 
                MessageHelper.Retrieved(EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<FinancialYearMaster>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
