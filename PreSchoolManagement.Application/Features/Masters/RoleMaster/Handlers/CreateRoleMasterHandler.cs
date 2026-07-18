using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateRoleMasterHandler(
    IRoleMasterService service,
    IValidator<CreateRoleMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper)
    : IRequestHandler<CreateRoleMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        CreateRoleMasterCommand request,
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
        
        bool isExist = await service.IsExistsAsync(request.RoleName,OperationType.Add,null,cancellationToken);
        
        if (isExist)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity(
                    "Masters",
                    EntityDescription.Role.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map<RoleMaster>(request);

        entity.EntryBy = currentUser.UserId;
        entity.EntryDate = DateTime.UtcNow;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.RoleId,
            messageHelper.AddedEntity(
                "Masters",
                EntityDescription.Role.ToString()),
            (int)HttpStatusCode.Created
        );
    }
}