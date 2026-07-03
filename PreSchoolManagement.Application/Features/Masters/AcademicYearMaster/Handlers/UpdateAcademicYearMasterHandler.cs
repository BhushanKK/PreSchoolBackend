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

public class UpdateAcademicYearMasterHandler(IAcademicYearMasterService service, IValidator<UpdateAcademicYearMasterCommand> validator, IMapper mapper) : IRequestHandler<UpdateAcademicYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateAcademicYearMasterCommand request, CancellationToken cancellationToken)
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

        var existing = await service.GetByIdAsync(request.AcademicYearId, cancellationToken);
        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.AcademicYearName ?? string.Empty, 
            OperationType.Update, 
            request.AcademicYearId, cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.AlreadyExists(EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request, existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = request.UserId;

        await service.UpdateAsync(entity, cancellationToken);


        return ApiResponse<int>.SuccessResponse
        (
            entity.AcademicYearId, 
            MessageHelper.Updated(EntityDescription.AcademicYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
