using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateDesignationMasterHandler(
    IDesignationMasterService service,
    IValidator<UpdateDesignationMasterCommand>validator,
    IMapper mapper,ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateDesignationMasterCommand,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateDesignationMasterCommand request,CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.designation);

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
        var existing = await service.GetByIdAsync(request.DesignationId,cancellationToken);

        if(existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.designation.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.Designation ?? string.Empty,
            OperationType.Update,
            request.DesignationId,
            cancellationToken
        );

        if(exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.designation.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }
        
        var entity = mapper.Map(request,existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId;

        await service.UpdateAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.DesignationId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.designation.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}