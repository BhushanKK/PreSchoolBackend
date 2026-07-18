using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteSectionMasterHandler(
    ISectionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteSectionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteSectionMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Section.ToString());

        var entity = await service.GetByIdAsync(request.SectionId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Section.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.SectionId,
            messageHelper.DeletedEntity("Masters",EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}