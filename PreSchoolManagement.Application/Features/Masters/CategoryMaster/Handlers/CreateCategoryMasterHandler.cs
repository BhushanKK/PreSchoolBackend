using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateCategoryMasterHandler(
    ICategoryMasterService service,
    IValidator<CreateCategoryMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<CreateCategoryMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        CreateCategoryMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masterss",EntityDescription.Category.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message, (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(
            request.CategoryName ?? string.Empty,
            OperationType.Add,
            null,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.Category.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map<CategoryMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.CategoryId,
            messageHelper.AddedEntity("Masters",EntityDescription.Category.ToString()),
            (int)HttpStatusCode.Created);
    }
}