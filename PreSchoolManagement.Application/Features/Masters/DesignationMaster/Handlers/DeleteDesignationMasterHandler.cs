using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class DeleteDesignationMasterHandler(IDesignationMasterService service)
    : IRequestHandler<DeleteDesignationMasterCommand ,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteDesignationMasterCommand request, CancellationToken cancellationToken)
    {
        if(request.DesignationId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.designation.ToString()),
                (int)HttpStatusCode.BadRequest

            );
        }

        var existing = await service.GetByIdAsync(request.DesignationId,cancellationToken);
        if(existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.designation.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(existing,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.DesignationId,
            MessageHelper.Deleted(EntityDescription.designation.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}