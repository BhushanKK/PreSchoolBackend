using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateMenuMasterHandler(
    IMenuMasterService service,
    IValidator<CreateMenuMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper)
    : IRequestHandler<CreateMenuMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        CreateMenuMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ",
                validationResult.Errors.Select(x => x.ErrorMessage));

            return ApiResponse<int>.FailureResponse(
                message,
                (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(
            request.MenuName ?? string.Empty,
            request.ParentMenuId,
            OperationType.Update,
            request.MenuId,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.Menu.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map<MenuMaster>(request);

        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId;

        await service.CreateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.MenuId,
            messageHelper.AddedEntity("Masters",EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.Created
        );
    }
}