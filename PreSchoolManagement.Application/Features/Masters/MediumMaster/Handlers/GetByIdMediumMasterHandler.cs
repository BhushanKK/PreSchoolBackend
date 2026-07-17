using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdMediumMasterHandler(IMediumMasterService service)
: IRequestHandler<GetByIdMediumMasterQuery ,ApiResponse<MediumMaster?>>
{
    public async Task<ApiResponse<MediumMaster?>> Handle(
        GetByIdMediumMasterQuery request,CancellationToken cancellationToken)
    {
        var medium = await service.GetByIdAsync(request.MediumId,cancellationToken);

        if (medium is null)
        {
            return ApiResponse<MediumMaster?>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Medium.ToString()),
                (int)HttpStatusCode.NotFound);
            
        }

        return ApiResponse<MediumMaster?>.SuccessResponse(medium,
        MessageHelper.Retrieved(EntityDescription.Medium.ToString()),
        (int)HttpStatusCode.OK);
    }
}