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
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateAcademicYearMasterHandler(
    IAcademicYearMasterService service,
    IValidator<CreateAcademicYearMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localizer)
    : IRequestHandler<CreateAcademicYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        CreateAcademicYearMasterCommand request,
        CancellationToken cancellationToken)
    {
        localizer.Get("Masters", EntityDescription.AcademicYear.ToString());

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

        var exists = await service.IsExistsAsync(
            request.AcademicYearName ?? string.Empty,
            OperationType.Add,
            null,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            ( 
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map<AcademicYearMaster>(request);

        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        ( 
            entity.AcademicYearId, 
            messageHelper.AddedEntity("Masters", EntityDescription.AcademicYear.ToString()), (int)HttpStatusCode.Created
        );
    }
}