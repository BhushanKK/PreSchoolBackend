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

public class UpdateSchoolDetailsMasterHandler(
    ISchoolDetailsMasterService service,
    IValidator<UpdateSchoolDetailsMasterCommand> validator,
    IMapper mapper,
    IMessageHelper messageHelper,
    ILocalizationService localization,
    ICurrentUserService currentUser)
    : IRequestHandler<UpdateSchoolDetailsMasterCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(
        UpdateSchoolDetailsMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get(
            LocaleEnums.Masters.ToString(),
            EntityDescription.SchoolDetails.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ",
                validationResult.Errors.Select(x => x.ErrorMessage));

            return ApiResponse<Guid>.FailureResponse(
                message,
                (int)HttpStatusCode.BadRequest);
        }

        var entity = await service.GetForUpdateAsync(
            request.SchoolDetailsId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<Guid>.FailureResponse(
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolDetails.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        var isExists = await service.IsExistsAsync(
            request.SchoolName ?? string.Empty,
            OperationType.Update,
            request.SchoolDetailsId,
            cancellationToken);

        if (isExists)
        {
            return ApiResponse<Guid>.FailureResponse(
                messageHelper.AlreadyExistsEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolDetails.ToString()),
                (int)HttpStatusCode.Conflict);
        }

        // Update entity
        mapper.Map(request, entity);

        entity.ModifyBy = currentUser.UserId;
        entity.ModifyDate = DateTime.UtcNow;

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<Guid>.SuccessResponse(
            entity.SchoolDetailsId,
            messageHelper.UpdatedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolDetails.ToString()),
            (int)HttpStatusCode.OK);
    }
}