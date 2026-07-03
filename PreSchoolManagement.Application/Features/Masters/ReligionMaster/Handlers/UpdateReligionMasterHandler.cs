using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Shared.Utils;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;

using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Application.Features.Commands;

namespace SchoolAdmission.Application.Features.Handlers;

public class updateReligionMasterHandler(IReligionMasterService service, IValidator<UpdateReligionMasterCommand> validator, IMapper mapper) : IRequestHandler<UpdateReligionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateReligionMasterCommand request, CancellationToken cancellationToken)
    {
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
                MessageHelper.NotFound(EntityDescription.Religion.ToString()), 
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
                MessageHelper.AlreadyExists(EntityDescription.Religion.ToString()), 
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request, existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = 1; // Replace with actual user ID in a real application

        await service.UpdateAsync(entity, cancellationToken);
        return ApiResponse<int>.SuccessResponse
        (
            entity.ReligionId, 
            MessageHelper.Updated(EntityDescription.Religion.ToString())
        );
    }
}