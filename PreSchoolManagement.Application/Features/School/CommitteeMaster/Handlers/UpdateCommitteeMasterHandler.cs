using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateCommitteeMasterHandler(
    ICommitteeMasterService service,
    IValidator<UpdateCommitteeMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IFileStorageService fileStorage,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateCommitteeMasterCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(
        UpdateCommitteeMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join("|", validationResult.Errors.Select(x => x.ErrorMessage));

            return ApiResponse<Guid>.FailureResponse
            (
                message,
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.CommitteeId, cancellationToken);

        if (existing is null)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.committee.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync(
            request.CommitteeName ?? string.Empty,
            OperationType.Update,
            request.CommitteeId,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.committee.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var oldLogo = existing.Logo;
        var oldLogoPath = existing.LogoPath;

        mapper.Map(request, existing);

        if (!string.IsNullOrWhiteSpace(request.LogoPath))
        {
            if (request.LogoPath != oldLogoPath &&
                !string.IsNullOrWhiteSpace(oldLogoPath))
            {
                await fileStorage.DeleteAsync(oldLogoPath, cancellationToken);
            }

            existing.Logo = request.Logo;
            existing.LogoPath = request.LogoPath;
        }
        else
        {
            existing.Logo = oldLogo;
            existing.LogoPath = oldLogoPath;
        }

        existing.ModifyDate = DateTime.UtcNow;
        existing.ModifyBy = currentUser.UserId;

        await service.UpdateAsync(existing, cancellationToken);

        return ApiResponse<Guid>.SuccessResponse
        (
            existing.CommitteeId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.committee.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}