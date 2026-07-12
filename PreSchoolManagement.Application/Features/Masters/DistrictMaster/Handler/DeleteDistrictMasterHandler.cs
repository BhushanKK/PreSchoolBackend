using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteDistrictMasterHandler(IDistrictMasterService service)
    : IRequestHandler<DeleteDistrictMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteDistrictMasterCommand request, CancellationToken cancellationToken)
    {
        if ( request.DistrictId <=0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.District.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.DistrictId, cancellationToken);

        if ( existing is null)
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.District.ToString()),
                (int)HttpStatusCode.NotFound
            );
        
        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.DistrictId,
            MessageHelper.Deleted(EntityDescription.District.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}