using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using System.Net;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateMenuMasterHandler(
    IMenuMasterService service,
    IValidator<UpdateMenuMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser)
    : IRequestHandler<UpdateMenuMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        UpdateMenuMasterCommand request,
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

        var entity = await service.GetByIdAsync(
            request.MenuId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Menu.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        var exists = await service.IsExistsAsync(
            request.MenuName ?? string.Empty,
            request.ParentMenuId,
            OperationType.Update,
            request.MenuId,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.AlreadyExists(EntityDescription.Menu.ToString()),
                (int)HttpStatusCode.Conflict);
        }

        mapper.Map(request, entity);

        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId;

        await service.UpdateAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse(
            entity.MenuId,
            MessageHelper.Updated(EntityDescription.Menu.ToString()),
            (int)HttpStatusCode.OK);
    }
}