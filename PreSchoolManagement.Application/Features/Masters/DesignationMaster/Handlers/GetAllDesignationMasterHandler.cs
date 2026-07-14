using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllDesignationMasterHandler(IDesignationMasterService service)
    :IRequestHandler<GetAllDesignationMasterQuery,ApiResponse<List<DesignationMaster>>>
{
    public async Task<ApiResponse<List<DesignationMaster>>> Handle(GetAllDesignationMasterQuery request,
    CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.filter,cancellationToken);

        if(data !=null)
        {
            return ApiResponse<List<DesignationMaster>>.SuccessResponse
            (
                data,
                MessageHelper.Retrieved(EntityDescription.designation.ToString()),
                (int)HttpStatusCode.OK
            );
        }

        else
        {
            return ApiResponse<List<DesignationMaster>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.designation.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}