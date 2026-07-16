using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdDesignationMasterHandler(IDesignationMasterService service)
    : IRequestHandler<GetByIdDesignationMasterQuery, ApiResponse<DesignationMaster?>>
{
    public async Task<ApiResponse<DesignationMaster?>> Handle(GetByIdDesignationMasterQuery request,
        CancellationToken cancellationToken)
    {
        if (request.DesignationId <= 0)
        {
            return ApiResponse<DesignationMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(
                EntityDescription.designation.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data =await service.GetByIdAsync(request.DesignationId,cancellationToken);

        if (data is null)
        {
            return ApiResponse<DesignationMaster?>.FailureResponse
            (
                MessageHelper.NotFound(
                EntityDescription.designation.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }


        return ApiResponse<DesignationMaster?>.SuccessResponse
        (
            data,
            MessageHelper.Retrieved(
                EntityDescription.designation.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}