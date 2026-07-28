using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllSectionMasterHandler(
    ISectionMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllSectionMasterQuery, ApiResponse<PaginatedResult<SectionMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<SectionMaster>>> Handle(
        GetAllSectionMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Sections = await service.GetAllAsync(request.Request,cancellationToken);

        return ApiResponse<PaginatedResult<SectionMaster>>.SuccessResponse
        (
            Sections,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}