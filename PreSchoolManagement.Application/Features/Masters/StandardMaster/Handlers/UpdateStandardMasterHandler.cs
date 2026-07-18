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

public class UpdateStandardMasterHandler(
    IStandardMasterService service,
    IValidator<UpdateStandardMasterCommand> validator,
    IMapper mapper,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateStandardMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateStandardMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Standard.ToString());

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

        var entity = await service.GetByIdAsync(request.StandardId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Standard.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.StandardName ?? string.Empty,
            OperationType.Update,
            request.StandardId,
            cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.Standard.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        mapper.Map(request, entity);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.StandardId,
            messageHelper.UpdatedEntity("Masters", EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}