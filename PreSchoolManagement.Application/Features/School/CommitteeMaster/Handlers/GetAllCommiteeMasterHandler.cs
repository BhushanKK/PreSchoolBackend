using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllCommitteeMasterHandler(
    ICommitteeMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllCommitteeMasterQuery, ApiResponse<PaginatedResult<CommitteeMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<CommitteeMaster>>>Handle(GetAllCommitteeMasterQuery request,
    CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.Request,cancellationToken);

        if(data != null)
        {
            return ApiResponse<PaginatedResult<CommitteeMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Committee.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<PaginatedResult<CommitteeMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.Committee.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}