using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllSchoolDetailsMasterHandler(
    ISchoolDetailsMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllSchoolDetailsMasterQuery, ApiResponse<PaginatedResult<SchoolDetailsMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<SchoolDetailsMaster>>> Handle(
        GetAllSchoolDetailsMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get(
            LocaleEnums.Masters.ToString(),
            EntityDescription.SchoolDetails.ToString());

        var schoolDetails = await service.GetAllAsync(
            request.Request,
            cancellationToken);

        return ApiResponse<PaginatedResult<SchoolDetailsMaster>>.SuccessResponse(
            schoolDetails,
            messageHelper.RetrievedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolDetails.ToString()),
            (int)HttpStatusCode.OK);
    }
}