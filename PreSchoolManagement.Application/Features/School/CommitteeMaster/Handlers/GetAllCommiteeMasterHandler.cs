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

public class GetAllCommitteeMasterHandler(
    ICommitteeMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllCommitteeMasterQuery, ApiResponse<List<CommitteeMaster>>>
{
    public async Task<ApiResponse<List<CommitteeMaster>>>Handle(GetAllCommitteeMasterQuery request,
    CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.filter,cancellationToken);

        if(data != null)
        {
            return ApiResponse<List<CommitteeMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Committee.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<CommitteeMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.Committee.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}