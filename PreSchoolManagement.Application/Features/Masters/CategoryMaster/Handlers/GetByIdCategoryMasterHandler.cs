using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdCategoryMasterHandler(ICategoryMasterService service)
    : IRequestHandler<GetByIdCategoryMasterQuery, ApiResponse<CategoryMaster?>>
{
    public async Task<ApiResponse<CategoryMaster?>> Handle(
        GetByIdCategoryMasterQuery request,
        CancellationToken cancellationToken)
    {
        if (request.CategoryId <= 0)
        {
            return ApiResponse<CategoryMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.Category.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.CategoryId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<CategoryMaster?>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Category.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<CategoryMaster?>.SuccessResponse
        (
            data,
            MessageHelper.Retrieved(EntityDescription.Category.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}