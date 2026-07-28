using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateSchoolDetailsMasterHandler(
    ISchoolDetailsMasterService service,
    IValidator<CreateSchoolDetailsMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<CreateSchoolDetailsMasterCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(
        CreateSchoolDetailsMasterCommand request,
        CancellationToken cancellationToken)
    {
        localization.Get(
            LocaleEnums.Masters.ToString(),
            EntityDescription.SchoolDetails.ToString());

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var message = string.Join(" | ",
                validationResult.Errors.Select(x => x.ErrorMessage));

            return ApiResponse<Guid>.FailureResponse(
                message,
                (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(
            request.SchoolName ?? string.Empty,
            OperationType.Add,
            null,
            cancellationToken);

        if (exists)
        {
            return ApiResponse<Guid>.FailureResponse(
                messageHelper.AlreadyExistsEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolDetails.ToString()),
                (int)HttpStatusCode.Conflict);
        }

        var entity = mapper.Map<SchoolDetailsMaster>(request);

        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId;

        await service.AddAsync(entity, cancellationToken);

        return ApiResponse<Guid>.SuccessResponse(
            entity.SchoolDetailsId,
            messageHelper.AddedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolDetails.ToString()),
            (int)HttpStatusCode.Created);
    }
}