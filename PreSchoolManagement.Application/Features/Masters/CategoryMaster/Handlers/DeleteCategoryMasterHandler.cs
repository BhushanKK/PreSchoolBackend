using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteCategoryMasterHandler(ICategoryMasterService service)
    : IRequestHandler<DeleteCategoryMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteCategoryMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.CategoryId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.Category.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.CategoryId, cancellationToken);

        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Category.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.CategoryId,
            MessageHelper.Deleted(EntityDescription.Category.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}