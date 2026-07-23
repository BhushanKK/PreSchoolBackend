using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateMediumMasterHandler(
    IMediumMasterService service,
    IValidator<UpdateMediumMasterCommand> validator,
    IMapper mapper,
    IMessageHelper messageHelper,
    ILocalizationService localization,
    ICurrentUserService currentUser)
    : IRequestHandler<UpdateMediumMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateMediumMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(), EntityDescription.Medium.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message,
            (int)HttpStatusCode.BadRequest);
        }

        var entity = await service.GetForUpdateAsync(request.MediumId, cancellationToken);
        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(), EntityDescription.Medium.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExists = await service.IsExistsAsync(request.MediumName ?? string.Empty,
        OperationType.Update, request.MediumId, cancellationToken);

        if (isExists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity(LocaleEnums.Masters.ToString(), EntityDescription.Medium.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }
        
        //update master
        mapper.Map(request,entity);

        var userId = currentUser.UserId;
        var currentDate = DateTime.UtcNow;

        entity.ModifyBy = userId;
        entity.ModifyDate = currentDate;

        //synchronize translations
        foreach( var dto in request.Translations)
        {
            var translation = entity.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LanguageCode);

            if(translation == null)
            {
                entity.Translations.Add(new MediumTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    MediumName = dto.MediumName
                });
            }
            else
                translation.MediumName = dto.MediumName;
        }

        //Remove Deleted Translations
        var removedTranslations = entity.Translations
            .Where(x => !request.Translations
            .Any(x => x.LanguageCode == x.LanguageCode))
            .ToList();

        foreach (var translation in removedTranslations)
            entity.Translations.Remove(translation);

        await service.UpdateAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.MediumId,
            messageHelper.UpdatedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Medium.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}