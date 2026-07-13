using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdHolidayMasterHandler(IHolidayMasterService service)
    : IRequestHandler<GetByIdHolidayMasterQuery, ApiResponse<HolidayMaster?>>
{
    public async Task<ApiResponse<HolidayMaster?>> Handle(
        GetByIdHolidayMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Holiday = await service.GetByIdAsync(request.HolidayId, cancellationToken);

        if (Holiday is null)
        {
            return ApiResponse<HolidayMaster?>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Holiday.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<HolidayMaster?>.SuccessResponse(
            Holiday,
            MessageHelper.Retrieved(EntityDescription.Holiday.ToString()),
            (int)HttpStatusCode.OK);
    }
}