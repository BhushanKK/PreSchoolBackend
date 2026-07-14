using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllStateMasterHandler(IStateMasterService service)
    :IRequestHandler<GetAllStateMasterQuery, ApiResponse<List<StateMaster>>>
{
    public async Task<ApiResponse<List<StateMaster>>> Handle(
        GetAllStateMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.filter,cancellationToken);

        if(data != null)
        {
            return ApiResponse<List<StateMaster>>.SuccessResponse
            (
                data,
                MessageHelper.Retrieved(EntityDescription.State.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<StateMaster>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.State.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}