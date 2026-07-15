using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteCommitteeMasterHandler(ICommitteeMasterService service)
: IRequestHandler<DeleteCommitteeMasterCommand,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteCommitteeMasterCommand request,CancellationToken cancellationToken)
    {
        if(request.CommitteeId <=0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.committee.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }
        var entity = await service.GetByIdAsync(request.CommitteeId,cancellationToken );
        if(entity is null)
        {
            return ApiResponse<int>.FailureResponse(MessageHelper.NotFound(EntityDescription.committee.ToString()),
            (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.CommitteeId,
            MessageHelper.Deleted(EntityDescription.committee.ToString()),
            (int)HttpStatusCode.OK
        );

    }
}