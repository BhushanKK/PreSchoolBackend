using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class updateReligionMasterHandler(IReligionMasterService service,
    IValidator<UpdateReligionMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateReligionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateReligionMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Religion.ToString());

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

        var existing = await service.GetByIdAsync(request.ReligionId, cancellationToken);
        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.Religion ?? string.Empty,
            OperationType.Update,
            request.ReligionId, cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request, existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId ?? null;

        await service.UpdateAsync(entity, cancellationToken);
        return ApiResponse<int>.SuccessResponse
        (
            entity.ReligionId,
            messageHelper.UpdatedEntity("Masters", EntityDescription.Religion.ToString())
        );
    }
}