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

public class UpdateMenuMasterHandler(
    IMenuMasterService service,
    IValidator<UpdateMenuMasterCommand> validator,
    ICurrentUserService currentUser,
    IMapper mapper,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateMenuMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateMenuMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            return ApiResponse<int>.FailureResponse(
                string.Join(" | ", validationResult.Errors.Select(x => x.ErrorMessage)),
                (int)HttpStatusCode.BadRequest);
        }

        var entity = await service.GetForUpdateAsync(
            request.MenuId,
            cancellationToken);

        if (entity == null)
        {
            return ApiResponse<int>.FailureResponse(
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.Menu.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        var exists = await service.IsExistsAsync(
            request.MenuName,
            request.ParentMenuId,
            OperationType.Update,
            request.MenuId,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse(
                messageHelper.AlreadyExistsEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.Menu.ToString()),
                (int)HttpStatusCode.Conflict);
        }

        // Update master fields
        mapper.Map(request, entity);

        entity.ModifyBy = currentUser.UserId;
        entity.ModifyDate = DateTime.UtcNow;

        // Update translations
        foreach (var dto in request.Translations)
        {
            var translation = entity.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LanguageCode);

            if (translation == null)
            {
                translation = new MenuTranslation
                {
                    MenuId = entity.MenuId,
                    LanguageCode = dto.LanguageCode
                };

                entity.Translations.Add(translation);
            }

            translation.MenuName = dto.MenuName;
        }

        // Remove deleted translations
        var removedTranslations = entity.Translations
            .Where(x => request.Translations.All(t => t.LanguageCode != x.LanguageCode))
            .ToList();

        foreach (var translation in removedTranslations)
        {
            entity.Translations.Remove(translation);
        }

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.MenuId,
            messageHelper.UpdatedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.OK);
    }
}