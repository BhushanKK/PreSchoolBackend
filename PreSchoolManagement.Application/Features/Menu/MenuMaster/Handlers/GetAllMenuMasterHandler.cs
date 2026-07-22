using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllMenuMasterQueryHandler(
    IMenuMasterService menuMasterService,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllMenuMasterQuery, ApiResponse<List<MenuMasterQueryDto>>>
{
    public async Task<ApiResponse<List<MenuMasterQueryDto>>> Handle(
        GetAllMenuMasterQuery request,
        CancellationToken cancellationToken)
    {
        var data = await menuMasterService.GetAllAsync(
            request.filter,
            cancellationToken);

        return ApiResponse<List<MenuMasterQueryDto>>.SuccessResponse(
            data,
            messageHelper.RetrievedEntity(
                "Masters",
                EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.OK);
    }
}