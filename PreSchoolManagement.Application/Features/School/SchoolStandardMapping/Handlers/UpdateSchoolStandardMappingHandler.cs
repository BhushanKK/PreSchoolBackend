using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateSchoolStandardMappingHandler(
    ISchoolStandardMappingService service,
    IValidator<UpdateSchoolStandardMappingCommand> validator,
    ICurrentUserService currentUser,
    IMapper mapper,
    IMessageHelper messageHelper)
    : IRequestHandler<UpdateSchoolStandardMappingCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(
        UpdateSchoolStandardMappingCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                (int)HttpStatusCode.BadRequest
            );
        }

        var entity = await service.GetForUpdateAsync(
            request.SchoolStandardMappingId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolStandardMapping.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var isExist = await service.IsExistsAsync
        (
            request.SchoolRegistrationId,
            request.StandardId,
            OperationType.Update,
            request.SchoolStandardMappingId,
            cancellationToken
        );

        if (isExist)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolStandardMapping.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        mapper.Map(request, entity);

        entity.ModifyBy = currentUser.UserId;
        entity.ModifyDate = DateTime.UtcNow;

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<Guid>.SuccessResponse
        (
            entity.SchoolStandardMappingId,
            messageHelper.UpdatedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolStandardMapping.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}