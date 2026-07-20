using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class updateReligionMasterHandler(IReligionMasterService service,
    IValidator<UpdateReligionMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateReligionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateReligionMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Religion.ToString());

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

        var entity = await service.GetForUpdateAsync(request.ReligionId, cancellationToken);
        if(entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExists = await service.IsExistsAsync
        (
            request.ReligionName ?? string.Empty,
            OperationType.Update,
            request.ReligionId, 
            cancellationToken
        );

        if (isExists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        //Update master
        mapper.Map(request, entity);

        var userId = currentUser.UserId;
        var currentDate= DateTime.UtcNow;

        entity.ModifyBy = userId;
        entity.ModifyDate = currentDate;

        //Synchronize Translations
        foreach(var dto in request.Translations)
        {
            var translation = entity.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LanguageCode);
            
            if (translation == null)
            {
                entity.Translations.Add(new ReligionTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    ReligionName = dto.ReligionName
                });
            }
            else
                translation.ReligionName = dto.ReligionName;      
        }

        //Remove deleted Translations

        var removedTranslations = entity.Translations
            .Where(x => !request.Translations
            .Any(t => t.LanguageCode == x.LanguageCode))
            .ToList();

        foreach(var translation in removedTranslations)
            entity.Translations.Remove(translation);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.ReligionId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.Religion.ToString()),
            (int)HttpStatusCode.OK
        );    
        
    }
}