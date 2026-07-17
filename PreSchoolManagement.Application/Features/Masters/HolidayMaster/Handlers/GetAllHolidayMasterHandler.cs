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

public class GetAllHolidayMasterHandler(IHolidayMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllHolidayMasterQuery, ApiResponse<List<HolidayMaster>>>
{
    public async Task<ApiResponse<List<HolidayMaster>>> Handle(
        GetAllHolidayMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Holiday.ToString());

        var Holidays = await service.GetAllAsync(cancellationToken);

        return ApiResponse<List<HolidayMaster>>.SuccessResponse
        (
            Holidays,
            messageHelper.RetrievedEntity("Masters", EntityDescription.Holiday.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}