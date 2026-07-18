using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdDivisionMasterHandler(
    IDivisionMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetByIdDivisionMasterQuery, ApiResponse<DivisionMaster?>>
{
    public async Task<ApiResponse<DivisionMaster?>> Handle(
        GetByIdDivisionMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Division = await service.GetByIdAsync(request.DivisionId, cancellationToken);

        if (Division is null)
        {
            return ApiResponse<DivisionMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Division.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<DivisionMaster?>.SuccessResponse
        (
            Division,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}