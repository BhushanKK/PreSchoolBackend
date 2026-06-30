using MediatR;
using System.Net;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolAdmission.Application.Features.CasteMasters.Commands;

namespace SchoolAdmission.Application.Features.Masters.Handlers;

public class DeleteCasteMasterHandler(ICasteMasterService service) : IRequestHandler<DeleteCasteMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteCasteMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.CasteId <= 0)
            return ApiResponse<int>.FailureResponse("Invalid caste id.", (int)HttpStatusCode.BadRequest);

        var existing = await service.GetByIdAsync(request.CasteId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse("Caste not found.", (int)HttpStatusCode.NotFound);

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse(request.CasteId, "Caste deleted successfully.", (int)HttpStatusCode.OK);
    }
}
