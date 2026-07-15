using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateCommitteeMasterHandler(
    ICommitteeMasterService service,
    IValidator<UpdateCommitteeMasterCommand>validator,
    IMapper mapper,ICurrentUserService currentUser)
    : IRequestHandler<UpdateCommitteeMasterCommand,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateCommitteeMasterCommand request ,CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request,cancellationToken);
        if(!validationResult.IsValid)
        {
            var message = string.Join("|",validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse
            (
                message,
                (int)HttpStatusCode.BadRequest
            );
        }
        var existing = await service.GetByIdAsync(request.CommitteeId,cancellationToken);
        if(existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.committee.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.CommitteeName ?? string.Empty,
            OperationType.Update,
            request.CommitteeId,
            cancellationToken
        );

        if(exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.AlreadyExists(EntityDescription.committee.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }
        var entity = mapper.Map(request,existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy=currentUser.UserId;

        await service.UpdateAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.CommitteeId,
            MessageHelper.Updated(EntityDescription.committee.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}