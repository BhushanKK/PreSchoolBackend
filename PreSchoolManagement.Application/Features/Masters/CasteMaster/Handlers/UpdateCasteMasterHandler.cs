using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolAdmission.Application.Features.CasteMasters.Commands;

namespace SchoolAdmission.Application.Features.Handlers;

public class UpdateCasteMasterHandler(ICasteMasterService service, IValidator<UpdateCasteMasterCommand> validator, IMapper mapper) : IRequestHandler<UpdateCasteMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateCasteMasterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message, (int)HttpStatusCode.BadRequest);
        }

        var existing = await service.GetByIdAsync(request.CasteId, cancellationToken);
        if (existing is null)
            return ApiResponse<int>.FailureResponse("Caste not found.", (int)HttpStatusCode.NotFound);

        var exists = await service.IsExistsAsync(request.Caste ?? string.Empty, SchoolAdmission.Domain.Utils.OperationType.Update, request.CasteId, cancellationToken);

        if (exists)
            return ApiResponse<int>.FailureResponse("Caste already exists.", (int)HttpStatusCode.Conflict);

        var entity = mapper.Map(request, existing);
        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.CasteID, "Caste updated successfully.", (int)HttpStatusCode.OK);
    }
}
