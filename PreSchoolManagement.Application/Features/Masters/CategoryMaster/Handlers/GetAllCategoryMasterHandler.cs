using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllCategoryMasterHandler(
    ICategoryMasterService service,
    IMessageHelper messageHelper)
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
                messageHelper.RetrievedEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<CategoryMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}