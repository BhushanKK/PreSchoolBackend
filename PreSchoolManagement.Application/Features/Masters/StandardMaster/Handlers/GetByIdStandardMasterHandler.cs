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

public class GetByIdStandardMasterHandler(
    IStandardMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetByIdStandardMasterQuery, ApiResponse<StandardMaster?>>
{
    public async Task<ApiResponse<StandardMaster?>> Handle(
        GetByIdStandardMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Standard.ToString());

        var Standard = await service.GetByIdAsync(request.StandardID, cancellationToken);

        if (Standard is null)
        {
            return ApiResponse<StandardMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Standard.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<StandardMaster?>.SuccessResponse
        (
            Standard,
            messageHelper.RetrievedEntity("Masters", EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}