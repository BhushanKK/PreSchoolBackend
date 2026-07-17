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

public class GetByIdHolidayMasterHandler(IHolidayMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetByIdHolidayMasterQuery, ApiResponse<HolidayMaster?>>
{
    public async Task<ApiResponse<HolidayMaster?>> Handle(
        GetByIdHolidayMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Holiday.ToString());

        var Holiday = await service.GetByIdAsync(request.HolidayId, cancellationToken);

        if (Holiday is null)
        {
            return ApiResponse<HolidayMaster?>.FailureResponse(
                messageHelper.NotFoundEntity("Masters",EntityDescription.Holiday.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<HolidayMaster?>.SuccessResponse
        (
            Holiday,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Holiday.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}