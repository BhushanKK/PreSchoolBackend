using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteReligionMasterHandler(IReligionMasterService service) 
    : IRequestHandler<DeleteReligionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteReligionMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.ReligionId <= 0)
            return ApiResponse<int>.FailureResponse
            (
                "Invalid religion id.", 
                (int)HttpStatusCode.BadRequest
            );

        var existing = await service.GetByIdAsync(request.ReligionId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                "Religion not found.", 
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.ReligionId, 
            MessageHelper.Deleted(EntityDescription.Religion.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}