using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteChairmanMasterHandler(
    IChairmanMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteChairmanMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteChairmanMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString() ,EntityDescription.Chairman.ToString());

        if (request.ChairmanId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Chairman.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.ChairmanId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Chairman.ToString()),
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.ChairmanId, 
            messageHelper.DeletedEntity(LocaleEnums.Masters.ToString() ,EntityDescription.Chairman.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
