using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdMediumMasterHandler(
    IMediumMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetByIdMediumMasterQuery ,ApiResponse<MediumMaster?>>
{
    public async Task<ApiResponse<MediumMaster?>> Handle(
        GetByIdMediumMasterQuery request,CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString());

        var medium = await service.GetByIdAsync(request.MediumId,cancellationToken);

        if (medium is null)
        {
            return ApiResponse<MediumMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<MediumMaster?>.SuccessResponse
        (
            medium,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}