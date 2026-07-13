using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllHolidayMasterHandler(IHolidayMasterService service)
    : IRequestHandler<GetAllHolidayMasterQuery, ApiResponse<List<HolidayMaster>>>
{
    public async Task<ApiResponse<List<HolidayMaster>>> Handle(
        GetAllHolidayMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Holidays = await service.GetAllAsync(cancellationToken);

        return ApiResponse<List<HolidayMaster>>.SuccessResponse(
            Holidays,
            MessageHelper.Retrieved(EntityDescription.Holiday.ToString()),
            (int)HttpStatusCode.OK);
    }
}