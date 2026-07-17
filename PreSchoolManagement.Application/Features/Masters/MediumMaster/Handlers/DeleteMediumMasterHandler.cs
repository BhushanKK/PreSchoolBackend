using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteMediumMasterHandler (IMediumMasterService service)
:IRequestHandler<DeleteMediumMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle (DeleteMediumMasterCommand request,CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(request.MediumId,cancellationToken);
         
        if(entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Medium.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.MediumId,
        MessageHelper.Deleted(EntityDescription.Medium.ToString()),
        (int)HttpStatusCode.OK);
    }
}