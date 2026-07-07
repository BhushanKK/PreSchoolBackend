using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdSectionMasterHandler(ISectionMasterService service)
    : IRequestHandler<GetByIdSectionMasterQuery, ApiResponse<SectionMaster?>>
{
    public async Task<ApiResponse<SectionMaster?>> Handle(
        GetByIdSectionMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Section = await service.GetByIdAsync(request.SectionId, cancellationToken);

        if (Section is null)
        {
            return ApiResponse<SectionMaster?>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Section.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<SectionMaster?>.SuccessResponse(
            Section,
            MessageHelper.Retrieved(EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK);
    }
}