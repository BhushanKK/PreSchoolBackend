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
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateHolidayMasterHandler(
    IHolidayMasterService service,
    IValidator<UpdateHolidayMasterCommand> validator,
    IMapper mapper,
    IMessageHelper messageHelper,
    ILocalizationService localization,
    ICurrentUserService currentUser)
    : IRequestHandler<UpdateHolidayMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateHolidayMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Holiday.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));

            return ApiResponse<int>.FailureResponse(
                message,
                (int)HttpStatusCode.BadRequest);
        }

        var entity = await service.GetForUpdateAsync(request.HolidayId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Holiday.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExists = await service.IsExistsAsync(
            request.HolidayName ?? string.Empty,
            OperationType.Update,
            request.HolidayId,
            cancellationToken);

        if (isExists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.Holiday.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        //Update Master
        mapper.Map(request, entity);

        var userId = currentUser.UserId;
        var currentDate = DateTime.UtcNow;

        entity.ModifyBy = userId;
        entity.ModifyDate = currentDate;

        //Synchronize translations
        foreach (var dto in request.Translations)
        {
            var translation = entity.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LanguageCode);
            
            if(translation == null)
            {
                entity.Translations.Add(new HolidayTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    HolidayName = dto.HolidayName
                });
            }
            else 
                translation.HolidayName = dto.HolidayName;  
        }
        //Remove Deleted translations
        var removedTranslations = entity.Translations
            .Where(x => !request.Translations
            .Any(t => t.LanguageCode == x.LanguageCode)) 
            .ToList();

        foreach(var translation in removedTranslations)
            entity.Translations.Remove(translation);

        await service.UpdateAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.HolidayId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.Holiday.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}