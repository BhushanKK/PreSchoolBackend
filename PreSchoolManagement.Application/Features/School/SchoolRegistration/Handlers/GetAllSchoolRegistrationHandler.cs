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

public class  GetAllSchoolRegistrationHandler(
    ISchoolRegistrationService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllSchoolRegistrationQuery, ApiResponse<PaginatedResult<SchoolRegistration>>>
{
    public async Task<ApiResponse<PaginatedResult<SchoolRegistration>>> Handle(
        GetAllSchoolRegistrationQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get(
            LocaleEnums.Masters.ToString(),
            EntityDescription.SchoolRegistrations.ToString());

        var schoolRegistrations = await service.GetAllAsync(
            request.Request,
            cancellationToken
        );

        return ApiResponse<PaginatedResult<SchoolRegistration>> .SuccessResponse(
            schoolRegistrations,
            messageHelper.RetrievedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolRegistrations.ToString()),
                (int)HttpStatusCode.OK);
    }
}