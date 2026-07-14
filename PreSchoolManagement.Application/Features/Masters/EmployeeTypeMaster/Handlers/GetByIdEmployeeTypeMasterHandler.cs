using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdEmployeeTypeMasterHandler(IEmployeeTypeMasterService service)
    : IRequestHandler<GetByIdEmployeeTypeMasterQuery,ApiResponse<EmployeeTypeMaster>>
{
    public async Task<ApiResponse<EmployeeTypeMaster?>> Handle(
        GetByIdEmployeeTypeMasterQuery request,
        CancellationToken cancellationToken)
    {
        if (request.EmployeeTypeId <=0)
        {
            return ApiResponse<EmployeeTypeMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.BadRequest
            );

        }
        var data = await service.GetByIdAsync(request.EmployeeTypeId,cancellationToken);

        if(data is null)
        {
            return ApiResponse<EmployeeTypeMaster?>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<EmployeeTypeMaster?>.SuccessResponse
        (
            data,
            MessageHelper.Retrieved(EntityDescription.EmployeeType.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}
