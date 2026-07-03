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

public class CreateCasteMasterHandler(ICasteMasterService service, IValidator<CreateCasteMasterCommand> validator, IMapper mapper) : IRequestHandler<CreateCasteMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateCasteMasterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message, (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(request.Caste ?? string.Empty, OperationType.Add, null, cancellationToken);
        if (exists)
            return ApiResponse<int>.FailureResponse(MessageHelper.AlreadyExists(EntityDescription.Caste.ToString()), (int)HttpStatusCode.Conflict);

        var entity = mapper.Map<CasteMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = Guid.NewGuid();

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.CasteID, MessageHelper.Added(EntityDescription.Caste.ToString()), (int)HttpStatusCode.Created);
    }
}
