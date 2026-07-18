using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdStateMasterHandler(
    IStateMasterService service,
    IMessageHelper messageHelper)
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
                messageHelper.InvalidIdEntity("Masters", EntityDescription.State.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.StateId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<StateMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.State.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<StateMaster?>.SuccessResponse
        (
            data,
            messageHelper.RetrievedEntity("Masters", EntityDescription.State.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}