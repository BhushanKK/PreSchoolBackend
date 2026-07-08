using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllStandardMasterHandler(IStandardMasterService service)
    : IRequestHandler<GetAllStandardMasterQuery, ApiResponse<List<StandardMaster>>>
{
    public async Task<ApiResponse<List<StandardMaster>>> Handle(
        GetAllStandardMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Standards = await service.GetAllAsync(cancellationToken);

        return ApiResponse<List<StandardMaster>>.SuccessResponse(
            Standards,
            MessageHelper.Retrieved(EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK);
    }
}