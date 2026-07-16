using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteMenuMasterHandler(
    IMenuMasterService service)
    : IRequestHandler<DeleteMenuMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteMenuMasterCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(
            request.MenuId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Menu.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(
            request.MenuId,
            cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            request.MenuId,
            MessageHelper.Deleted(EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.OK);
    }
}