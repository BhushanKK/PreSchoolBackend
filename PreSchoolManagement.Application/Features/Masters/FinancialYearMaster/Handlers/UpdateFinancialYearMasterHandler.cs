using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateFinancialYearMasterHandler(IFinancialYearMasterService service, 
    IValidator<UpdateFinancialYearMasterCommand> validator, IMapper mapper
    ,ICurrentUserService currentUser) 
    : IRequestHandler<UpdateFinancialYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateFinancialYearMasterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse
            (
                message, 
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.FinancialYearId, cancellationToken);
        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        var exists = await service.IsExistsAsync
        (
            request.FinancialYearName ?? string.Empty, 
            OperationType.Update, 
            request.FinancialYearId, cancellationToken
        );

        if (exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.AlreadyExists(EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map(request, existing);
        entity.ModifyDate = DateTime.UtcNow;
        entity.ModifyBy = currentUser.UserId ?? null;

        await service.UpdateAsync(entity, cancellationToken);


        return ApiResponse<int>.SuccessResponse
        (
            entity.FinancialYearId, 
            MessageHelper.Updated(EntityDescription.FinancialYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
