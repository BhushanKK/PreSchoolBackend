using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteCasteMasterHandler(ICasteMasterService service)
    : IRequestHandler<DeleteCasteMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteCasteMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.CasteId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.Caste.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.CasteId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.Caste.ToString()), 
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.CasteId, 
            MessageHelper.Deleted(EntityDescription.Caste.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
