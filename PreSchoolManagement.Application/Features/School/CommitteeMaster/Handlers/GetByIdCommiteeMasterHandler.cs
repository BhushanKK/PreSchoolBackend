using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdCommitteeMasterHandler(
    ICommitteeMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetByIdCommitteeMasterQuery, ApiResponse<CommitteeMaster>>
{
    public async Task<ApiResponse<CommitteeMaster>> Handle(
        GetByIdCommitteeMasterQuery request,
        CancellationToken cancellationToken)
    {
        if (request.CommitteeId == Guid.Empty)
        {
            return ApiResponse<CommitteeMaster>.FailureResponse(
                messageHelper.InvalidIdEntity("Masters",EntityDescription.Committee.ToString()),
                (int)HttpStatusCode.BadRequest);
        }

        var data = await service.GetByIdAsync(request.CommitteeId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<CommitteeMaster>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Committee.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<CommitteeMaster>.SuccessResponse
        (
            data,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Committee.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}