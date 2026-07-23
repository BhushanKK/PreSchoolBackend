using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteMediumMasterHandler (
    IMediumMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    :IRequestHandler<DeleteMediumMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle (DeleteMediumMasterCommand request,CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString());

        var entity = await service.GetByIdAsync(request.MediumId,cancellationToken);
         
        if(entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.MediumId,
            messageHelper.DeletedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}