using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteDivisionMasterHandler(
    IDivisionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteDivisionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteDivisionMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Division.ToString());

        var entity = await service.GetByIdAsync(request.DivisionId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Division.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.DivisionId,
            messageHelper.DeletedEntity("Masters",EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}