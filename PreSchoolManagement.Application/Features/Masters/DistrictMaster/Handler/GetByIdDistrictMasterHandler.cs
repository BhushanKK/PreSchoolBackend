using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdDistrictMasterHandler(
    IDistrictMasterService service,
    IMessageHelper messageHelper)
    :IRequestHandler<GetByIdDistrictMasterQuery, ApiResponse<DistrictMaster?>>
{
    public async Task<ApiResponse<DistrictMaster?>> Handle(GetByIdDistrictMasterQuery request, CancellationToken cancellationToken)
    {
        if(request.DistrictId <= 0)
        {
            return ApiResponse<DistrictMaster?>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.District.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.DistrictId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<DistrictMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.District.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<DistrictMaster?>.SuccessResponse
        (
            data,
            messageHelper.RetrievedEntity("Masters",EntityDescription.District.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}