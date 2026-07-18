using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateCasteMasterHandler(
    ICasteMasterService service, 
    IValidator<UpdateCasteMasterCommand> validator, 
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization) 
    : IRequestHandler<UpdateCasteMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateCasteMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters" ,EntityDescription.Caste.ToString());

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

        var existing = await service.GetByIdAsync(request.CasteId, cancellationToken);
        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters" ,EntityDescription.Caste.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.Caste ?? string.Empty, 
            OperationType.Update, 
            request.CasteId, cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters" ,EntityDescription.Caste.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request, existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId ?? null;

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.CasteID, 
            messageHelper.UpdatedEntity("Masters" ,EntityDescription.Caste.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}
