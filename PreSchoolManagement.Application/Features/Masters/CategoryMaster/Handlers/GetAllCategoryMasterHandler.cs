using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllCategoryMasterHandler(ICategoryMasterService service)
    : IRequestHandler<GetAllCategoryMasterQuery, ApiResponse<List<CategoryMaster>>>
{
    public async Task<ApiResponse<List<CategoryMaster>>> Handle(
        GetAllCategoryMasterQuery request,
        CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.filter,cancellationToken);

        if (data != null)
        {
            return ApiResponse<List<CategoryMaster>>.SuccessResponse
            (
                data,
                MessageHelper.Retrieved(EntityDescription.Category.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<CategoryMaster>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Category.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}