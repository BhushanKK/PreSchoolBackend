using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;
using Org.BouncyCastle.Ocsp;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateStateMasterHandler(
    IStateMasterService service,
    IValidator<UpdateStateMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateStateMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
    UpdateStateMasterCommand request, 
    CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join("|", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse
            (
                message,
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.StateId, cancellationToken);

        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.State.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.StateName ?? string.Empty,
            OperationType.Update,
            request.StateId,
            cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.State.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        //updata master
        mapper.Map(request, existing);
        
        var userId = currentUser.UserId;
        var currentDate = DateTime.UtcNow;

        existing.ModifyBy = userId;
        existing.ModifyDate = currentDate;

        //Synchronize translations
        foreach (var dto in request.Translations)
        {
            var translation = existing.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LangauageCode);
            
            if (translation == null)
            {
                existing.Translations.Add(new StateTranslation
                {
                    LanguageCode = dto.LangauageCode,
                    StateName = dto.StateName
                });
            }
            else
                translation.StateName = dto.StateName;
        }

        //Remove deleted translations
        var removedTranslations = existing.Translations
            .Where(x => !request.Translations
            .Any(t => t.LangauageCode == x.LanguageCode))
            .ToList();
        
        foreach (var translation in removedTranslations)
            existing.Translations.Remove(translation);

        await service.UpdateAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            existing.StateId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.State.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}