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

public class UpdateDivisionMasterHandler(
    IDivisionMasterService service,
    IValidator<UpdateDivisionMasterCommand> validator,
    IMapper mapper)
    : IRequestHandler<UpdateDivisionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateDivisionMasterCommand request,
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

        var entity = await service.GetByIdAsync(request.DivisionId, cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Division.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        var exists = await service.IsExistsAsync(
            request.DivisionName ?? string.Empty,
            OperationType.Update,
            request.DivisionId,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.AlreadyExists(EntityDescription.Division.ToString()),
                (int)HttpStatusCode.Conflict);
        }

        mapper.Map(request, entity);

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.DivisionId,
            MessageHelper.Updated(EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK);
    }
}