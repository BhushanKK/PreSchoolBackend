using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateFinancialYearMasterHandler(
    IFinancialYearMasterService service,
    IValidator<UpdateFinancialYearMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<UpdateFinancialYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateFinancialYearMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.FinancialYear.ToString());
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
                messageHelper.NotFoundEntity("Masters", EntityDescription.FinancialYear.ToString()),
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
                messageHelper.AlreadyExistsEntity("Masters", EntityDescription.FinancialYear.ToString()),
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
            messageHelper.UpdatedEntity("Masters", EntityDescription.FinancialYear.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}
