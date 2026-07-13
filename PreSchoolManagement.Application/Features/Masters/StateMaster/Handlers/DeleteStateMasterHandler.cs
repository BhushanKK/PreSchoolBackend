using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteStateMasterHandler(IStateMasterService service)
    :IRequestHandler<DeleteStateMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteStateMasterCommand request,CancellationToken cancellationToken)
    {
        if (request.StateId <=0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.State.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.StateId, cancellationToken);
        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.State.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(existing,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.StateId,
            MessageHelper.Deleted(EntityDescription.State.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}