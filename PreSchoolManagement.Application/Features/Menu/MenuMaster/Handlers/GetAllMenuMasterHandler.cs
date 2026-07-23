using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllMenuMasterQueryHandler(
    IMenuMasterService menuMasterService,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllMenuMasterQuery, ApiResponse<PaginatedResult<MenuMasterQueryDto>>>
{
    public async Task<ApiResponse<PaginatedResult<MenuMasterQueryDto>>> Handle(
        GetAllMenuMasterQuery request,
        CancellationToken cancellationToken)
    {
        var result = await menuMasterService.GetAllAsync(
            request.Request,
            cancellationToken);

        return ApiResponse<PaginatedResult<MenuMasterQueryDto>>.SuccessResponse(
            result,
            messageHelper.RetrievedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.OK);
    }
}