using MediatR;
using AutoMapper;
using FluentValidation;
using SchoolManagement.Domain.Entities;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolAdmission.Application.Features.CasteMasters.Commands;
using System.Net;

namespace SchoolAdmission.Application.Features.Handlers;

public class CreateCasteMasterHandler(ICasteMasterService service, IValidator<CreateCasteMasterCommand> validator, IMapper mapper) : IRequestHandler<CreateCasteMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateCasteMasterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message, 400);
        }

        var exists = await service.IsExistsAsync(request.Caste ?? string.Empty, SchoolAdmission.Domain.Utils.OperationType.Add, null, cancellationToken);
        if (exists)
            return ApiResponse<int>.FailureResponse("Caste already exists.", 409);

        var entity = mapper.Map<CasteMaster>(request);

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.CasteID, "Caste created successfully.", (int)HttpStatusCode.Created);
    }
}
