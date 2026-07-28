using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateDistrictMasterHandler(
    IDistrictMasterService service,
    IValidator<UpdateDistrictMasterCommand> validator,
    ICurrentUserService currentUser,
    IMapper mapper,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateDistrictMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateDistrictMasterCommand request,
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

        var entity = await service.GetForUpdateAsync(request.DistrictId,cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.District.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExist = await service.IsExistsAsync
        (
            request.DistrictName,
            OperationType.Update,
            request.DistrictId,
            cancellationToken
        );

        if (isExist)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.District.ToString()),
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
                entity.Translations.Add(new DistrictTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    DistrictName = dto.DistrictName
                });
            }
            else
                translation.DistrictName = dto.DistrictName;
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
            entity.DistrictId,
            messageHelper.UpdatedEntity(LocaleEnums.Masters.ToString(),EntityDescription.District.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}