using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteDivisionMasterHandler(IDivisionMasterService service)
    : IRequestHandler<DeleteDivisionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteDivisionMasterCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(request.DivisionId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Division.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.DivisionId,
            MessageHelper.Deleted(EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK);
    }
}