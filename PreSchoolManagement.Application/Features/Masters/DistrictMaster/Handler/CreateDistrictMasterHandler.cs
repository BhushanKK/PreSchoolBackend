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

public class CreateDistrictMasterHandler(
    IDistrictMasterService service,
    IValidator<CreateDistrictMasterCommand> validator , 
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
:IRequestHandler<CreateDistrictMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateDistrictMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.District.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message,(int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(request.DistrictName ?? string.Empty,OperationType.Add,null, cancellationToken);
        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.District.ToString()), 
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map<DistrictMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId ?? null;

        await service.AddAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.DistrictId, 
            messageHelper.AddedEntity("Masters",EntityDescription.District.ToString()), 
            (int)HttpStatusCode.Created
        );
    }
}