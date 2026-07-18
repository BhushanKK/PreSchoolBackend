using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteStandardMasterHandler(
    IStandardMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteStandardMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteStandardMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Standard.ToString());

        var entity = await service.GetByIdAsync(request.StandardId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Standard.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.StandardId,
            messageHelper.DeletedEntity("Masters", EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}