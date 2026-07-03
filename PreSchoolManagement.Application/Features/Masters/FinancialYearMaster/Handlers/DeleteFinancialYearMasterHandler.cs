using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteFinancialYearMasterHandler(IFinancialYearMasterService service) : IRequestHandler<DeleteFinancialYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteFinancialYearMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.FinancialYearId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.FinancialYearId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.FinancialYearId, 
            MessageHelper.Deleted(EntityDescription.FinancialYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
