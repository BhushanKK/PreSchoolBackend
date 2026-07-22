using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteCommitteeMasterHandler(
    ICommitteeMasterService service, 
    IFileStorageService fileStorage,
    IMessageHelper messageHelper)
    : IRequestHandler<DeleteCommitteeMasterCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(DeleteCommitteeMasterCommand request,CancellationToken cancellationToken)
    {
        if (request.CommitteeId == Guid.Empty)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.committee.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var entity = await service.GetByIdAsync(request.CommitteeId,cancellationToken);

        if (entity is null)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.committee.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        // Delete logo from folder
        if (!string.IsNullOrWhiteSpace(entity.LogoPath))
            await fileStorage.DeleteAsync(entity.LogoPath,cancellationToken);

        // Delete database record
        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<Guid>.SuccessResponse
        (
            entity.CommitteeId,
            messageHelper.DeletedEntity("Masters",EntityDescription.committee.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}