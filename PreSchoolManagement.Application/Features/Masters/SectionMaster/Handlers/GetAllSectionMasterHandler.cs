using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllSectionMasterHandler(
    ISectionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllSectionMasterQuery, ApiResponse<List<SectionMaster>>>
{
    public async Task<ApiResponse<List<SectionMaster>>> Handle(
        GetAllSectionMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Section.ToString());

        var Sections = await service.GetAllAsync(request.filter,cancellationToken);

        return ApiResponse<List<SectionMaster>>.SuccessResponse
        (
            Sections,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}