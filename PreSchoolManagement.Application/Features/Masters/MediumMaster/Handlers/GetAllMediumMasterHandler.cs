using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllMediumMasterHandler(IMediumMasterService service)
:IRequestHandler<GetAllMediumMasterQuery, ApiResponse<List<MediumMaster>>>
{
    public async Task<ApiResponse<List<MediumMaster>>> Handle(GetAllMediumMasterQuery request,CancellationToken cancellationToken)
    {
        var mediums = await service.GetAllAsync(cancellationToken);
        return ApiResponse<List<MediumMaster>>.SuccessResponse(mediums,
        MessageHelper.Retrieved(EntityDescription.Medium.ToString()),
        (int)HttpStatusCode.OK);
    }
}