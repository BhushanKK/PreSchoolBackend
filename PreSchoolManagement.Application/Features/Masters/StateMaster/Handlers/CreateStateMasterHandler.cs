using MediatR;
using AutoMapper;
using System.Net;
using FluentValidation;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateStateMasterHandler(IStateMasterService service,
IValidator<CreateStateMasterCommand> validator, IMapper mapper,
ICurrentUserService currentUser)
: IRequestHandler<CreateStateMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateStateMasterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if(!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message, (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(request.StateName ?? string.Empty, OperationType.Add,null,cancellationToken);
        if (exists)
            return ApiResponse<int>.FailureResponse(MessageHelper.AlreadyExists(EntityDescription.State.ToString()), (int)HttpStatusCode.Conflict);

        var entity = mapper.Map<StateMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId ?? null;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.StateId, MessageHelper.Added(EntityDescription.State.ToString()),(int)HttpStatusCode.Created);
    }
}