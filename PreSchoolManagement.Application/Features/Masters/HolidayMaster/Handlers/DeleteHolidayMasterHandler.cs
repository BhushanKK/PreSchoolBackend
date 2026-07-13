using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteHolidayMasterHandler(IHolidayMasterService service)
    : IRequestHandler<DeleteHolidayMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteHolidayMasterCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(request.HolidayId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Holiday.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.HolidayId,
            MessageHelper.Deleted(EntityDescription.Holiday.ToString()),
            (int)HttpStatusCode.OK);
    }
}