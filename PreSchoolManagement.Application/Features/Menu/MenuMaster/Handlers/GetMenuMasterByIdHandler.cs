using System.Net;
using AutoMapper;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetMenuMasterByIdHandler(
    IMenuMasterService menuMasterService,
    IMapper mapper,
    IMessageHelper messageHelper)
    : IRequestHandler<GetMenuMasterByIdQuery, ApiResponse<MenuMasterDto?>>
{
    public async Task<ApiResponse<MenuMasterDto?>> Handle(
        GetMenuMasterByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await menuMasterService.GetByIdAsync(request.MenuId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<MenuMasterDto?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Menu.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var dto = mapper.Map<MenuMasterDto?>(entity);

        return ApiResponse<MenuMasterDto?>.SuccessResponse
        (
            dto,
            messageHelper.RetrievedEntity("Masters", EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}