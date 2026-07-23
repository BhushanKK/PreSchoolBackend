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

public class GetByIdSectionMasterHandler(
    ISectionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetByIdSectionMasterQuery, ApiResponse<SectionMaster?>>
{
    public async Task<ApiResponse<SectionMaster?>> Handle(
        GetByIdSectionMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(), EntityDescription.Section.ToString());

        var Section = await service.GetByIdAsync(request.SectionId, cancellationToken);

        if (Section is null)
        {
            return ApiResponse<SectionMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(), EntityDescription.Section.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<SectionMaster?>.SuccessResponse
        (
            Section,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}