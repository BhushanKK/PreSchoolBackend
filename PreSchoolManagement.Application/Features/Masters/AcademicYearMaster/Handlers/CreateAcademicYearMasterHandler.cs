using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateAcademicYearMasterHandler(
    IAcademicYearMasterService service,
    IValidator<CreateAcademicYearMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper)
    : IRequestHandler<CreateAcademicYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        CreateAcademicYearMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return ApiResponse<int>.FailureResponse
            (
                string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                (int)HttpStatusCode.BadRequest
            );
        }

        bool isExist = await service.IsExistsAsync(
            request.AcademicYearName,
            OperationType.Add,
            null,
            cancellationToken);

        if (isExist)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity(
                    "Masters",
                    EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map<AcademicYearMaster>(request);

        entity.EntryBy = currentUser.UserId;
        entity.EntryDate = DateTime.UtcNow;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.AcademicYearId,
            messageHelper.AddedEntity(
                "Masters",
                EntityDescription.AcademicYear.ToString()),
            (int)HttpStatusCode.Created
        );
    }
}