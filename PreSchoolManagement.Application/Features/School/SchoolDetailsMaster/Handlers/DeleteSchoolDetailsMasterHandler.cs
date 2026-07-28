using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteSchoolDetailsMasterHandler(
    ISchoolDetailsMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteSchoolDetailsMasterCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(
        DeleteSchoolDetailsMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get(
            LocaleEnums.Masters.ToString(),
            EntityDescription.SchoolDetails.ToString());

        var entity = await service.GetByIdAsync(
            request.SchoolDetailsId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<Guid>.FailureResponse(
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolDetails.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<Guid>.SuccessResponse(
            entity.SchoolDetailsId,
            messageHelper.DeletedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolDetails.ToString()),
            (int)HttpStatusCode.OK);
    }
}