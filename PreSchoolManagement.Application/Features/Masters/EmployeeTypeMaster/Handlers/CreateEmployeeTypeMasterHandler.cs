using MediatR;
using AutoMapper;
using System.Net;
using FluentValidation;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreatEmployeeTypeMaster(
    IEmployeeTypeMasterService service,
    IValidator<CreateEmployeeTypeMasterCommand> validator, 
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
: IRequestHandler<CreateEmployeeTypeMasterCommand, ApiResponse<int>>
{
    public  async Task<ApiResponse<int>> Handle(CreateEmployeeTypeMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.EmployeeType.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            var message = string.Join("|",validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse
            (
                message, 
                (int)HttpStatusCode.BadRequest
            );
        }

        var exists = await service.IsExistsAsync(request.EmployeeTypeName ?? string.Empty,OperationType.Add,null,cancellationToken);
        
        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }
        var entity = mapper.Map<EmployeeTypeMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy=currentUser.UserId ?? null;

        await service.AddAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.EmployeeTypeId,
            messageHelper.AddedEntity("Masters",EntityDescription.EmployeeType.ToString()),
            (int)HttpStatusCode.Created
        );
    }
}