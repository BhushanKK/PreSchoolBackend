using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetCategoryDropdownQueryHandler(
    ICategoryMasterService roleMasterService,
    IMessageHelper messageHelper)
    : IRequestHandler<GetCategoryDropdownQuery, ApiResponse<List<CategoryDropdownDto>>>
{
    public async Task<ApiResponse<List<CategoryDropdownDto>>> Handle(
        GetCategoryDropdownQuery request,
        CancellationToken cancellationToken)
    {
        var castes = await roleMasterService.GetActiveCategoriesAsync(cancellationToken);

        return ApiResponse<List<CategoryDropdownDto>>.SuccessResponse
        (
            castes,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.Caste.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}