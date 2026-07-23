using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteCasteMasterHandler(
    ICasteMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteCasteMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteCasteMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString() ,EntityDescription.Caste.ToString());

        if (request.CasteId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Caste.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.CasteId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Caste.ToString()),
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.CasteId, 
            messageHelper.DeletedEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Caste.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
