using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllSectionMasterHandler(
    ISectionMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllSectionMasterQuery, ApiResponse<List<SectionMaster>>>
{
    public async Task<ApiResponse<List<SectionMaster>>> Handle(
        GetAllSectionMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Sections = await service.GetAllAsync(request.filter,cancellationToken);

        return ApiResponse<List<SectionMaster>>.SuccessResponse
        (
            Sections,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}