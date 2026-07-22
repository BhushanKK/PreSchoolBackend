using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed class GetParentMenuQueryHandler(
    IMenuMasterService menuMasterService,
    IMessageHelper messageHelper)
    : IRequestHandler<GetParentMenuQuery, ApiResponse<List<ParentMenuDto>>>
{
    public async Task<ApiResponse<List<ParentMenuDto>>> Handle(
        GetParentMenuQuery request,
        CancellationToken cancellationToken)
    {
        var parentMenus = await menuMasterService.GetParentMenusAsync(cancellationToken);

        return ApiResponse<List<ParentMenuDto>>.SuccessResponse
        (
            parentMenus,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}