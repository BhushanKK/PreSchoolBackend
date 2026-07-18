using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllStateMasterHandler(
    IStateMasterService service,
    IMessageHelper messageHelper)
    :IRequestHandler<GetAllStateMasterQuery, ApiResponse<List<StateMaster>>>
{
    public async Task<ApiResponse<List<StateMaster>>> Handle(
        GetAllStateMasterQuery request, CancellationToken cancellationToken)
    {
        
        var data = await service.GetAllAsync(cancellationToken);

        if(data != null)
        {
            return ApiResponse<List<StateMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity("Masters",EntityDescription.State.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<StateMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.State.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}