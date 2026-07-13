using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateStateMasterHandler(
    IStateMasterService service,
IValidator<UpdateStateMasterCommand> validator,
IMapper mapper,
ICurrentUserService currentUser)
    : IRequestHandler<UpdateStateMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateStateMasterCommand request,CancellationToken cancellationToken)
    {
        var validationResult= await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            var message = string.Join("|",validationResult.Errors.Select(e => e.ErrorMessage));

            return ApiResponse<int>.FailureResponse
            (
                message,
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.StateId,cancellationToken);

        if(existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.State.ToString()),
                (int)HttpStatusCode.NotFound
            );      
        }

        var exists = await service.IsExistsAsync
        (
            request.StateName ?? string.Empty,
            OperationType.Update,
            request.StateId,
            cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.AlreadyExists(EntityDescription.State.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request, existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId;

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.StateId,
            MessageHelper.Updated(EntityDescription.State.ToString()),
            (int)HttpStatusCode.OK
        );
    }   
}