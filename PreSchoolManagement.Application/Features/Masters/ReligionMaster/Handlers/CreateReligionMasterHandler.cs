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

public class CreateReligionMasterHandler(IReligionMasterService service, IValidator<CreateReligionMasterCommand> validator, IMapper mapper) : IRequestHandler<CreateReligionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateReligionMasterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message, (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(request.Religion ?? string.Empty, OperationType.Add, null, cancellationToken);
        
        if (exists)
            return ApiResponse<int>.FailureResponse(MessageHelper.AlreadyExists(EntityDescription.Religion.ToString()), (int)HttpStatusCode.Conflict);

        var entity = mapper.Map<ReligionMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = 1;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.ReligionId, MessageHelper.Added(EntityDescription.Religion.ToString()), (int)HttpStatusCode.Created);
    }
}
