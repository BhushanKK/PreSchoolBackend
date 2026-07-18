using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;
public class GetByIdCategoryMasterHandler(
    ICategoryMasterService service,
    IMessageHelper messageHelper)
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
                messageHelper.InvalidIdEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.CategoryId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<CategoryMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<CategoryMaster?>.SuccessResponse
        (
            data,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Category.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}