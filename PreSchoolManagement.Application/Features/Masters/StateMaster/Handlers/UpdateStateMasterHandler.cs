using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
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

        var entity = await service.GetForUpdateAsync(request.StateId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.State.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExists = await service.IsExistsAsync
        (
            request.StateName ,
            OperationType.Update,
            request.StateId,
            cancellationToken
        );

        if (isExists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.State.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        //updata master
        mapper.Map(request, entity);
    
        entity.ModifyBy = currentUser.UserId;
        entity.ModifyDate = DateTime.UtcNow;

        //Synchronize translations
        foreach (var dto in request.Translations)
        {
            var translation = entity.Translations
                .FirstOrDefault(x => x.LanguageCode == dto.LanguageCode);
            
            if (translation == null)
            {
                entity.Translations.Add(new StateTranslation
                {
                    LanguageCode = dto.LanguageCode,
                    StateName = dto.StateName
                });
            }
            else
                translation.StateName = dto.StateName;
        }

        //Remove deleted translations
        var removedTranslations = entity.Translations
            .Where(x => !request.Translations
            .Any(t => t.LanguageCode == x.LanguageCode))
            .ToList();
        
        foreach (var translation in removedTranslations)
            entity.Translations.Remove(translation);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.StateId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.State.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}