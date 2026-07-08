using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdStandardMasterHandler(IStandardMasterService service)
    : IRequestHandler<GetByIdStandardMasterQuery, ApiResponse<StandardMaster?>>
{
    public async Task<ApiResponse<StandardMaster?>> Handle(
        GetByIdStandardMasterQuery request,
        CancellationToken cancellationToken)
    {
        var Standard = await service.GetByIdAsync(request.StandardID, cancellationToken);

        if (Standard is null)
        {
            return ApiResponse<StandardMaster?>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Standard.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<StandardMaster?>.SuccessResponse(
            Standard,
            MessageHelper.Retrieved(EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK);
    }
}