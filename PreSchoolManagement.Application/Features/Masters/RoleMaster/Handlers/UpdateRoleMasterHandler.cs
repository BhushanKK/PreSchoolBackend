using MediatR;
using AutoMapper;
using System.Net;
using FluentValidation;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateRoleMasterHandler(
    IRoleMasterService service,
    IValidator<UpdateRoleMasterCommand> validator,
    IMapper mapper,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateRoleMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateRoleMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Role.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));

            return ApiResponse<int>.FailureResponse
            (
                message,
                (int)HttpStatusCode.BadRequest
            );
        }

        var entity = await service.GetByIdAsync(request.RoleId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Role.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync(
            request.RoleName ?? string.Empty,
            OperationType.Update,
            request.RoleId,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.Role.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        mapper.Map(request, entity);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.RoleId,
            messageHelper.UpdatedEntity("Masters", EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}