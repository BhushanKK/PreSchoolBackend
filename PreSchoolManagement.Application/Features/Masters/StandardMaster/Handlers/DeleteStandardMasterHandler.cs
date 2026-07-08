using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteStandardMasterHandler(IStandardMasterService service)
    : IRequestHandler<DeleteStandardMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteStandardMasterCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(request.StandardId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Standard.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.StandardId,
            MessageHelper.Deleted(EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK);
    }
}