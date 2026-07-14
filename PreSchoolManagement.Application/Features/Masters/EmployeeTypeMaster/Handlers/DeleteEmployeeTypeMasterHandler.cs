using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteEmployeeTypeMasterHandler(IEmployeeTypeMasterService service)
    :IRequestHandler<DeleteEmployeeTypeMasterCommand,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteEmployeeTypeMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.EmployeeTypeId <=0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.BadRequest

            );
        }

        var existing = await service.GetByIdAsync(request.EmployeeTypeId, cancellationToken);
        if(existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(existing,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.EmployeeTypeId,
            MessageHelper.Deleted(EntityDescription.EmployeeType.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}