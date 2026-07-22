using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateCommitteeMasterHandler(
    ICommitteeMasterService service,
    IValidator<CreateCommitteeMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper)
    : IRequestHandler<CreateCommitteeMasterCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateCommitteeMasterCommand request,CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request,cancellationToken);

        if (!validationResult.IsValid)
        {
            var message =string.Join("|", validationResult.Errors.Select(x => x.ErrorMessage));
            
            return ApiResponse<Guid>.FailureResponse
            (
                message,
                (int)HttpStatusCode.BadRequest
            );
        }

        var exists = await service.IsExistsAsync(request.CommitteeName ?? string.Empty,
            OperationType.Add,null,cancellationToken);

        if (exists)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.Committee.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map<CommitteeMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId;

        await service.AddAsync(entity,cancellationToken);

        return ApiResponse<Guid>.SuccessResponse
        (
            entity.CommitteeId,
            messageHelper.AddedEntity("Masters",EntityDescription.Committee.ToString()),
            (int)HttpStatusCode.Created
        );
    }
}