using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteSectionMasterHandler(ISectionMasterService service)
    : IRequestHandler<DeleteSectionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteSectionMasterCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(request.SectionId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Section.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.SectionId,
            MessageHelper.Deleted(EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK);
    }
}