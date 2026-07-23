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

public class GetAllStateMasterHandler(
    IStateMasterService service,
    IMessageHelper messageHelper)
    :IRequestHandler<GetAllStateMasterQuery, ApiResponse<PaginatedResult<StateMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<StateMaster>>> Handle(
        GetAllStateMasterQuery request, CancellationToken cancellationToken)
    {
        
        var data = await service.GetAllAsync(request.Request,cancellationToken);

        if(data != null)
        {
            return ApiResponse<PaginatedResult<StateMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.State.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<PaginatedResult<StateMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.State.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}