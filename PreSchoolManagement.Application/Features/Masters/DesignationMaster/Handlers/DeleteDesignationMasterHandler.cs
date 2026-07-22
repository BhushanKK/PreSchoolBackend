using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class DeleteDesignationMasterHandler(
    IDesignationMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<DeleteDesignationMasterCommand ,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteDesignationMasterCommand request, CancellationToken cancellationToken)
    {
        if(request.DesignationId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.Designation.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.DesignationId,cancellationToken);
        if(existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Designation.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(existing,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.DesignationId,
            messageHelper.DeletedEntity("Masters",EntityDescription.Designation.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}