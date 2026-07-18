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

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateSectionMasterHandler(
    ISectionMasterService service,
    IValidator<UpdateSectionMasterCommand> validator,
    IMapper mapper,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateSectionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateSectionMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Section.ToString());

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

        var entity = await service.GetByIdAsync(request.SectionId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Section.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.SectionName ?? string.Empty,
            OperationType.Update,
            request.SectionId,
            cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.Section.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        mapper.Map(request, entity);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.SectionId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}