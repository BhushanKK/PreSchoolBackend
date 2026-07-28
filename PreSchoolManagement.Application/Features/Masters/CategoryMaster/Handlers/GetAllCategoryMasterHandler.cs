using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllCategoryMasterHandler(
    ICategoryMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllCategoryMasterQuery,
      ApiResponse<PaginatedResult<CategoryMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<CategoryMaster>>> Handle(
        GetAllCategoryMasterQuery request,
        CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(request.Request,cancellationToken);

        return ApiResponse<PaginatedResult<CategoryMaster>>.SuccessResponse
        (
            result,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),
            EntityDescription.AcademicYear.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}