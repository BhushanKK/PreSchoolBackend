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

public class UpdateChairmanMasterHandler(
    IChairmanMasterService service,
    IValidator<UpdateChairmanMasterCommand> validator,
    ICurrentUserService currentUser,
    IMapper mapper,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateChairmanMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateChairmanMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return ApiResponse<int>.FailureResponse
            (
                string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                (int)HttpStatusCode.BadRequest
            );
        }

        var entity = await service.GetForUpdateAsync(request.ChairmanId,cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.Chairman.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExist = await service.IsExistsAsync
        (
            request.ChairmanName,
            OperationType.Update,
            request.ChairmanId,
            cancellationToken
        );

        if (isExist)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity(LocaleEnums.Masters.ToString(),EntityDescription.Chairman.ToString()),
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
                entity.Translations.Add(new ChairmanTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    ChairmanName = dto.ChairmanName
                });
            }
            else
                translation.ChairmanName = dto.ChairmanName;
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
            entity.ChairmanId,
            messageHelper.UpdatedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Chairman.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}