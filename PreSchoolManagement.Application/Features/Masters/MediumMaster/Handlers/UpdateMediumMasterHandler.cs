using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateMediumMasterHandler(IMediumMasterService service,IValidator<UpdateMediumMasterCommand>validator,IMapper mapper)
: IRequestHandler<UpdateMediumMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateMediumMasterCommand request,CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request,cancellationToken);

        if(!validationResult.IsValid)
        {
            var message = string.Join(" | ",validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message,
            (int)HttpStatusCode.BadRequest);
        }

        var entity = await service.GetByIdAsync(request.MediumId,cancellationToken);
        if(entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Medium.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync(request.Medium ?? string.Empty,
        OperationType.Update,request.MediumId,cancellationToken);

        if(exists)
        {
            return ApiResponse<int>.FailureResponse(MessageHelper.AlreadyExists(EntityDescription.Medium.ToString()),
            (int)HttpStatusCode.Conflict);
        }
        mapper.Map(request,entity);

        await service.UpdateAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.MediumId,MessageHelper.Updated(EntityDescription.Medium.ToString()),
        (int)HttpStatusCode.OK);

    }
}