using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteCategoryMasterHandler(
    ICategoryMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteCategoryMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteCategoryMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Category.ToString());

        if (request.CategoryId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.CategoryId, cancellationToken);

        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.CategoryId,
            messageHelper.DeletedEntity("Masters",EntityDescription.Category.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}