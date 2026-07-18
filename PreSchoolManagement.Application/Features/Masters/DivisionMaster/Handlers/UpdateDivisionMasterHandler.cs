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

public class UpdateDivisionMasterHandler(
    IDivisionMasterService service,
    IValidator<UpdateDivisionMasterCommand> validator,
    ICurrentUserService currentUser,
    IMapper mapper,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateDivisionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateDivisionMasterCommand request,
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

        var entity = await service.GetForUpdateAsync
        (
            request.DivisionId,
            cancellationToken
        );

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Division.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExist = await service.IsExistsAsync
        (
            request.DivisionName,
            OperationType.Update,
            request.DivisionId,
            cancellationToken
        );

        if (isExist)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.Division.ToString()),
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
                entity.Translations.Add(new DivisionTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    DivisionName = dto.DivisionName
                });
            }
            else
                translation.DivisionName = dto.DivisionName;
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
            entity.DivisionId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}