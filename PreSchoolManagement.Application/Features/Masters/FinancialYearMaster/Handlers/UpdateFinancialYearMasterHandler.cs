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

public class UpdateFinancialYearMasterHandler(
    IFinancialYearMasterService service,
    IValidator<UpdateFinancialYearMasterCommand> validator,
    ICurrentUserService currentUser,
    IMapper mapper,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateFinancialYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateFinancialYearMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return ApiResponse<int>.FailureResponse(
                string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                (int)HttpStatusCode.BadRequest
            );
        }

        var entity = await service.GetForUpdateAsync(
            request.FinancialYearId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExist = await service.IsExistsAsync(
            request.FinancialYearName,
            OperationType.Update,
            request.FinancialYearId,
            cancellationToken);

        if (isExist)
        {
            return ApiResponse<int>.FailureResponse(
                messageHelper.AlreadyExistsEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.FinancialYear.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        // Update master
        mapper.Map(request, entity);

        entity.ModifyBy = currentUser.UserId;
        entity.ModifyDate = DateTime.UtcNow;

        // Synchronize translations
        foreach (var dto in request.Translations)
        {
            var translation = entity.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LanguageCode);

            if (translation == null)
            {
                entity.Translations.Add(new FinancialYearTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    FinancialYearName = dto.FinancialYearName
                });
            }
            else
            {
                translation.FinancialYearName = dto.FinancialYearName;
            }
        }

        // Remove deleted translations
        var removedTranslations = entity.Translations
            .Where(x => !request.Translations
                .Any(t => t.LanguageCode == x.LanguageCode))
            .ToList();

        foreach (var translation in removedTranslations)
        {
            entity.Translations.Remove(translation);
        }

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.FinancialYearId,
            messageHelper.UpdatedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.FinancialYear.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}