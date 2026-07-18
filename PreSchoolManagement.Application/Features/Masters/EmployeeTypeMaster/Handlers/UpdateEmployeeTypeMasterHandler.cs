using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateEmployeeTypeMasterHandler(
    IEmployeeTypeMasterService service,
    IValidator<UpdateEmployeeTypeMasterCommand> validator,
    IMapper mapper,ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    :IRequestHandler<UpdateEmployeeTypeMasterCommand,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateEmployeeTypeMasterCommand request,CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.EmployeeType.ToString());

        var validationResult = await validator.ValidateAsync(request,cancellationToken);
        if(!validationResult.IsValid)
        {
            var message = string.Join("|",validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse
            (
                message,
                (int)HttpStatusCode.BadRequest
            );
        }
        var existing = await service.GetByIdAsync(request.EmployeeTypeId,cancellationToken);

        if(existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.EmployeeTypeName ?? string.Empty,
            OperationType.Update,
            request.EmployeeTypeId,
            cancellationToken
        );

        if(exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request,existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId;

        await service.UpdateAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.EmployeeTypeId,
            messageHelper.UpdatedEntity("Masters",EntityDescription.EmployeeType.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}