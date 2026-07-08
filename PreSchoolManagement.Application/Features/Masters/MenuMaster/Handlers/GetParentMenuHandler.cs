using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed class GetParentMenuQueryHandler(
    IMenuMasterService menuMasterService)
    : IRequestHandler<GetParentMenuQuery, ApiResponse<List<ParentMenuDto>>>
{
    public async Task<ApiResponse<List<ParentMenuDto>>> Handle(
        GetParentMenuQuery request,
        CancellationToken cancellationToken)
    {
        var parentMenus = await menuMasterService.GetParentMenusAsync(cancellationToken);

        return ApiResponse<List<ParentMenuDto>>.SuccessResponse(
            parentMenus,
            MessageHelper.Retrieved(EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.OK);
    }
}