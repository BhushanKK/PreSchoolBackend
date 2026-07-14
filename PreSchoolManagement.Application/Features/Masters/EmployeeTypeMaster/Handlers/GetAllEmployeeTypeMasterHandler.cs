using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllEmployeeTypeMasterHandler(IEmployeeTypeMasterService service)
    : IRequestHandler<GetAllEmployeeTypeMasterQuery, ApiResponse<List<EmployeeTypeMaster>>>
{
    public async Task<ApiResponse<List<EmployeeTypeMaster>>> Handle(GetAllEmployeeTypeMasterQuery request,
     CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.filter,cancellationToken);

        if(data !=null)
        {
            return ApiResponse<List<EmployeeTypeMaster>>.SuccessResponse
            (
                data,
                MessageHelper.Retrieved(EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<EmployeeTypeMaster>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.OK
            );
            
        }
    }
}