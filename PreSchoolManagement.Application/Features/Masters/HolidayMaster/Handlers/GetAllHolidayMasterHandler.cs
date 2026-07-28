using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllHolidayMasterHandler(IHolidayMasterService service,IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllHolidayMasterQuery, ApiResponse<PaginatedResult<HolidayMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<HolidayMaster>>> Handle(
        GetAllHolidayMasterQuery request, CancellationToken cancellationToken)
    {

        var Holidays = await service.GetAllAsync(request.Request, cancellationToken);
        localization.Get(LocaleEnums.Masters.ToString(), EntityDescription.Holiday.ToString());

        return ApiResponse<PaginatedResult<HolidayMaster>>.SuccessResponse
        (
            Holidays,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.Holiday.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}