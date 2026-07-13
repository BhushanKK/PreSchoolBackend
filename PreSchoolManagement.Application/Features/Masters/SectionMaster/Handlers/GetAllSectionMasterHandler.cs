using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllSectionMasterHandler(ISectionMasterService service)
    : IRequestHandler<GetAllSectionMasterQuery, ApiResponse<List<SectionMaster>>>
{
    public async Task<ApiResponse<List<SectionMaster>>> Handle(
        GetAllSectionMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Sections = await service.GetAllAsync(request.filter,cancellationToken);

        return ApiResponse<List<SectionMaster>>.SuccessResponse(
            Sections,
            MessageHelper.Retrieved(EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK);
    }
}