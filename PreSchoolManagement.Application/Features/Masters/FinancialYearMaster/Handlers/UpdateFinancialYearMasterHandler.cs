using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateFinancialYearMasterHandler(
    IFinancialYearMasterService service,
    IValidator<UpdateFinancialYearMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateFinancialYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateFinancialYearMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.FinancialYear.ToString());
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

        var entity = await service.GetForUpdateAsync(request.FinancialYearId, cancellationToken);
        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExists = await service.IsExistsAsync
        (
            request.FinancialYearName ?? string.Empty,
            OperationType.Update,
            request.FinancialYearId, cancellationToken
        );

        if (isExists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        // Update master
        mapper.Map(request, entity);

        var userId = currentUser.UserId;
        var currentDate = DateTime.UtcNow;

        entity.ModifyBy = userId;
        entity.ModifyDate = currentDate;

        //Synchronize Translations
        foreach(var dto in request.Translations)
        {
            var translation = entity.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LanguageCode);

            if(translation == null)
            {
                entity.Translations.Add(new FinancialYearTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    FinancialYearName = dto.FinancialYearName
                });
                
            }
            else
                translation.FinancialYearName = dto.FinancialYearName;

        }

        //Remove Deleted translations
        var removedTranslations =entity.Translations
            .Where(x => !request.Translations
            .Any(t => t.LanguageCode == x.LanguageCode))
            .ToList();

        foreach(var translation in removedTranslations)
            entity.Translations.Remove(translation);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.FinancialYearId,
            messageHelper.UpdatedEntity(
                "Masters",
                EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.OK

        );

    }
}
