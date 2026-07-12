using MediatR;
using AutoMapper;
using System.Net;
using FluentValidation;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateSectionMasterHandler(
    ISectionMasterService service,
    IValidator<UpdateSectionMasterCommand> validator,
    IMapper mapper)
    : IRequestHandler<UpdateSectionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateSectionMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));

            return ApiResponse<int>.FailureResponse(
                message,
                (int)HttpStatusCode.BadRequest);
        }

        var entity = await service.GetByIdAsync(request.SectionId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Section.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        var exists = await service.IsExistsAsync(
            request.SectionName ?? string.Empty,
            OperationType.Update,
            request.SectionId,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.AlreadyExists(EntityDescription.Section.ToString()),
                (int)HttpStatusCode.Conflict);
        }

        mapper.Map(request, entity);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.SectionId,
            MessageHelper.Updated(EntityDescription.Section.ToString()),
            (int)HttpStatusCode.OK);
    }
}