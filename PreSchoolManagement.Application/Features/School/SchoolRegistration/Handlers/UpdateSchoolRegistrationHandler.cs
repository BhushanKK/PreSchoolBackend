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

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateSchoolRegistrationHandler(
    ISchoolRegistrationService service,
    IValidator<UpdateSchoolRegistrationCommand> validator,
    IMapper mapper,
    IMessageHelper messageHelper,
    ILocalizationService localization,
    ICurrentUserService currentUser)
    : IRequestHandler<UpdateSchoolRegistrationCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(
        UpdateSchoolRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get(
            LocaleEnums.Masters.ToString(),
            EntityDescription.SchoolRegistrations.ToString());

        var validationResult = await validator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ",
                validationResult.Errors.Select(x => x.ErrorMessage));

            return ApiResponse<Guid>.FailureResponse(
                message,
                (int)HttpStatusCode.BadRequest);
        }

        var entity = await service.GetForUpdateAsync(
            request.SchoolRegistrationId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<Guid>.FailureResponse(
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolRegistrations.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        var isExists = await service.IsExistsAsync(
            request.SchoolName ?? string.Empty,
            OperationType.Update,
            request.SchoolRegistrationId,
            cancellationToken);

        if (isExists)
        {
            return ApiResponse<Guid>.FailureResponse(
                messageHelper.AlreadyExistsEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolRegistrations.ToString()),
                (int)HttpStatusCode.Conflict);
        }

        // Update Entity
        mapper.Map(request, entity);

        entity.ModifyBy = currentUser.UserId;
        entity.ModifyDate = DateTime.UtcNow;

        await service.UpdateAsync(
            entity,
            cancellationToken);

        return ApiResponse<Guid>.SuccessResponse(
            entity.SchoolRegistrationId,
            messageHelper.UpdatedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolRegistrations.ToString()),
            (int)HttpStatusCode.OK);
    }
}