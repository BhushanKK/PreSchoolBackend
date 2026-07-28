using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteReligionMasterHandler(
    IReligionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteReligionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteReligionMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString());

        if (request.ReligionId <= 0)
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.BadRequest
            );

        var existing = await service.GetByIdAsync(request.ReligionId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                "Religion not found.",
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.ReligionId,
            messageHelper.DeletedEntity(LocaleEnums.Masters.ToString(), EntityDescription.Religion.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}