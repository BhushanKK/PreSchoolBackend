using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdDesignationMasterHandler(
    IDesignationMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetByIdDesignationMasterQuery, ApiResponse<DesignationMaster?>>
{
    public async Task<ApiResponse<DesignationMaster?>> Handle(GetByIdDesignationMasterQuery request,
        CancellationToken cancellationToken)
    {
        if (request.DesignationId <= 0)
        {
            return ApiResponse<DesignationMaster?>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.Designation.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data =await service.GetByIdAsync(request.DesignationId,cancellationToken);

        if (data is null)
        {
            return ApiResponse<DesignationMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Designation.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<DesignationMaster?>.SuccessResponse
        (
            data,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Designation.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}