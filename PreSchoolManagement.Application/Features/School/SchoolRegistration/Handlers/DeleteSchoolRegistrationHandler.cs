using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteSchoolRegistrationHandler(ISchoolRegistrationService service,
IMessageHelper messageHelper,
ILocalizationService localization)
: IRequestHandler<DeleteSchoolRegistrationCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(DeleteSchoolRegistrationCommand request,
    CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(),
        EntityDescription.SchoolRegistrations.ToString());

        var entity = await service.GetByIdAsync(request.SchoolRegistrationId,cancellationToken);

        if(entity is null)
        {
            return ApiResponse<Guid>.FailureResponse(messageHelper.NotFoundEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolRegistrations.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(entity,cancellationToken);

        return ApiResponse<Guid>.SuccessResponse(entity.SchoolRegistrationId,
        messageHelper.DeletedEntity(
            LocaleEnums.Masters.ToString(),
            EntityDescription.SchoolRegistrations.ToString()),
            (int)HttpStatusCode.OK);
    
    }
}