using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateCategoryMasterHandler(
    ICategoryMasterService service,
    IValidator<UpdateCategoryMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateCategoryMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateCategoryMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Category.ToString());

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

        var existing = await service.GetByIdAsync(request.CategoryId, cancellationToken);

        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.CategoryName ?? string.Empty,
            OperationType.Update,
            request.CategoryId,
            cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request, existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId;

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.CategoryId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.Category.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}