using MediatR;
using AutoMapper;
using System.Net;
using FluentValidation;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Application.Features.Commands;

namespace SchoolAdmission.Application.Features.Handlers;

public class CreateAcademicYearMasterHandler(IAcademicYearMasterService service, IValidator<CreateAcademicYearMasterCommand> validator, IMapper mapper) : IRequestHandler<CreateAcademicYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateAcademicYearMasterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message, (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(request.AcademicYearName ?? string.Empty, OperationType.Add, null, cancellationToken);
        if (exists)
            return ApiResponse<int>.FailureResponse(MessageHelper.AlreadyExists(EntityDescription.AcademicYear.ToString()), (int)HttpStatusCode.Conflict);

        var entity = mapper.Map<AcademicYearMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = request.UserId;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.AcademicYearId, 
            MessageHelper.Added(EntityDescription.AcademicYear.ToString()), 
            (int)HttpStatusCode.Created
        );
    }
}
