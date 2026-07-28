using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllDesignationMasterHandler(
    IDesignationMasterService service,
    IMessageHelper messageHelper)
    :IRequestHandler<GetAllDesignationMasterQuery,ApiResponse<PaginatedResult<DesignationMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<DesignationMaster>>> Handle(GetAllDesignationMasterQuery request,
    CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(request.Request,cancellationToken);

        if(data !=null)
        {
            return ApiResponse<PaginatedResult<DesignationMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Designation.ToString()),
                (int)HttpStatusCode.OK
            );
        }

        else
        {
            return ApiResponse<PaginatedResult<DesignationMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.Designation.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}