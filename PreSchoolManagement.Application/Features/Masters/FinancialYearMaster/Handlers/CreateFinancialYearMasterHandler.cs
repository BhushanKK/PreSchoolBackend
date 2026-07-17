using MediatR;
using AutoMapper;
using System.Net;
using FluentValidation;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;
public class CreateFinancialYearMasterHandler(IFinancialYearMasterService service,
    IValidator<CreateFinancialYearMasterCommand> validator,
    IMapper mapper,ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization) 
    : IRequestHandler<CreateFinancialYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateFinancialYearMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.FinancialYear.ToString());
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message, (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(request.FinancialYearName ?? string.Empty, OperationType.Add, null, cancellationToken);
        
        if (exists)
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.FinancialYear.ToString()), 
                (int)HttpStatusCode.Conflict
            );

        var entity = mapper.Map<FinancialYearMaster>(request);
        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId ?? null;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.FinancialYearId, 
            messageHelper.AddedEntity("Masters",EntityDescription.FinancialYear.ToString()), 
            (int)HttpStatusCode.Created
        );
    }
}
