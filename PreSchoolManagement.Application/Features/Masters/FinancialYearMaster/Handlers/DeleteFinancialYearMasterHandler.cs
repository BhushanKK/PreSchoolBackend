using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteFinancialYearMasterHandler(
    IFinancialYearMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization) 
    : IRequestHandler<DeleteFinancialYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteFinancialYearMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.FinancialYear.ToString());
        if (request.FinancialYearId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.FinancialYearId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.FinancialYearId, 
            messageHelper.DeletedEntity("Masters",EntityDescription.FinancialYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
