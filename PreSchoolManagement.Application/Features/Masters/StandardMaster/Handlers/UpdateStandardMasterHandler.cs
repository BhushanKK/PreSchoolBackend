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

public class UpdateStandardMasterHandler(
    IStandardMasterService service,
    IValidator<UpdateStandardMasterCommand> validator,
    IMapper mapper)
    : IRequestHandler<UpdateStandardMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateStandardMasterCommand request,
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

        var entity = await service.GetByIdAsync(request.StandardId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Standard.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        var exists = await service.IsExistsAsync(
            request.StandardName ?? string.Empty,
            OperationType.Update,
            request.StandardId,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.AlreadyExists(EntityDescription.Standard.ToString()),
                (int)HttpStatusCode.Conflict);
        }

        mapper.Map(request, entity);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.StandardId,
            MessageHelper.Updated(EntityDescription.Standard.ToString()),
            (int)HttpStatusCode.OK);
    }
}