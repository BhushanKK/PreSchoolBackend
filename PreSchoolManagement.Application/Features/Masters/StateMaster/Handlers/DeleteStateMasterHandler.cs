using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteStateMasterHandler(
    IStateMasterService service,
    IMessageHelper messageHelper)
    :IRequestHandler<DeleteStateMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteStateMasterCommand request,CancellationToken cancellationToken)
    {
        
        
        if (request.StateId <=0)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.State.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.StateId, cancellationToken);
        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.State.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(existing,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.StateId,
            messageHelper.DeletedEntity("Masters",EntityDescription.State.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}