using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteRoleMasterHandler(IRoleMasterService service)
    : IRequestHandler<DeleteRoleMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteRoleMasterCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(request.RoleId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Role.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.RoleId,
            MessageHelper.Deleted(EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK);
    }
}