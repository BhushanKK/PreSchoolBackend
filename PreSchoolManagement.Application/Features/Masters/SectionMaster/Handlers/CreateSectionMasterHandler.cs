using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;
using System.Net;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateSectionMasterHandler(
    ISectionMasterService service,
    IValidator<CreateSectionMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper)
    : IRequestHandler<CreateSectionMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        CreateSectionMasterCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return ApiResponse<int>.FailureResponse(
                string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                (int)HttpStatusCode.BadRequest);
        }

        if (await service.IsExistsAsync(request.SectionName,OperationType.Add,null,cancellationToken))
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity(LocaleEnums.Masters.ToString(),EntityDescription.Section.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }

        var entity = mapper.Map<SectionMaster>(request);

        entity.EntryBy = currentUser.UserId;
        entity.EntryDate = DateTime.UtcNow;
        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.SectionId,
            messageHelper.AddedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Section.ToString()),
            (int)HttpStatusCode.Created
        );
    }
}