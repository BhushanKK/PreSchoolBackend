using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteHolidayMasterHandler(
    IHolidayMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteHolidayMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteHolidayMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Holiday.ToString());

        var entity = await service.GetByIdAsync(request.HolidayId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Holiday.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.HolidayId,
            messageHelper.DeletedEntity("Masters", EntityDescription.Holiday.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}