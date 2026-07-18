using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;
using System.Net;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateRoleMasterHandler(
    IRoleMasterService service,
    IValidator<UpdateRoleMasterCommand> validator,
    ICurrentUserService currentUser,
    IMapper mapper,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateRoleMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateRoleMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return ApiResponse<int>.FailureResponse(
                string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                (int)HttpStatusCode.BadRequest);
        }

        var entity = await service.GetForUpdateAsync(
            request.RoleId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                messageHelper.NotFoundEntity(
                    "Masters",
                    EntityDescription.Role.ToString()),
                (int)HttpStatusCode.NotFound);
        }
        var isExist = await service.IsExistsAsync(request.RoleName,OperationType.Update,
        request.RoleId,cancellationToken);

        if (isExist)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.Role.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        // Update master
        mapper.Map(request, entity);

        var userId = currentUser.UserId;
        var currentDate = DateTime.UtcNow;

        entity.ModifyBy = userId;
        entity.ModifyDate = currentDate;

        // Synchronize translations
        foreach (var dto in request.Translations)
        {
            var translation = entity.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LanguageCode);

            if (translation == null)
            {
                entity.Translations.Add(new RoleTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    RoleName = dto.RoleName
                });
            }
            else
                translation.RoleName = dto.RoleName;
        }

        // Remove deleted translations
        var removedTranslations = entity.Translations
            .Where(x => !request.Translations
            .Any(t => t.LanguageCode == x.LanguageCode))
            .ToList();

        foreach (var translation in removedTranslations)
            entity.Translations.Remove(translation);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.RoleId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}