using System.Net;
using MediatR;
using AutoMapper;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using FluentValidation;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateDistrictMasterHandler(
    IDistrictMasterService service,
    IValidator<UpdateDistrictMasterCommand> validator, 
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateDistrictMasterCommand, ApiResponse<int>>
{   
    public async Task<ApiResponse<int>> Handle(UpdateDistrictMasterCommand request, 
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.District.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e. ErrorMessage));
            return ApiResponse<int>.FailureResponse
            (
                message,
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.DistrictId,cancellationToken);
        if(existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.District.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.DistrictName ?? string.Empty,
            OperationType.Update,
            request.DistrictId, cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.District.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request, existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId ?? null;

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.DistrictId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.District.ToString()),
            (int)HttpStatusCode.OK

        );
    }
    
}
