using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdCommitteeMasterHandler(ICommitteeMasterService service)
    : IRequestHandler<GetByIdCommitteeMasterQuery, ApiResponse<CommitteeMaster>>
{
    public async Task<ApiResponse<CommitteeMaster?>> Handle(GetByIdCommitteeMasterQuery request, CancellationToken cancellationToken)
    {
        if (request.CommiteeId <=0)
        {
            return ApiResponse<CommitteeMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.committee.ToString()),
                (int)HttpStatusCode.BadRequest
            );

        }
        var data = await service.GetByIdAsync(request.CommiteeId,cancellationToken);
        if (data is null)
        {
            return ApiResponse<CommitteeMaster?>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.committee.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<CommitteeMaster?>.SuccessResponse
        (
            data,
            MessageHelper.Retrieved(EntityDescription.committee.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}