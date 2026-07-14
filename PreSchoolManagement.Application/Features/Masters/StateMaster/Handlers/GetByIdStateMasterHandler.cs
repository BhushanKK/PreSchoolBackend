using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdStateMasterHandler(IStateMasterService service)
    : IRequestHandler<GetByIdStateMasterQuery, ApiResponse<StateMaster?>>
{
    public async Task<ApiResponse<StateMaster?>> Handle(
        GetByIdStateMasterQuery request,
        CancellationToken cancellationToken)
    {
        if (request.StateId <= 0)
        {
            return ApiResponse<StateMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.State.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.StateId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<StateMaster?>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.State.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<StateMaster?>.SuccessResponse
        (
            data,
            MessageHelper.Retrieved(EntityDescription.State.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}