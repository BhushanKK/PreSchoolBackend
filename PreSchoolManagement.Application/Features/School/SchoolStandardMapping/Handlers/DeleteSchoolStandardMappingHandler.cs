using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteSchoolStandardMappingHandler(
    ISchoolStandardMappingService service,
    IMessageHelper messageHelper)
    : IRequestHandler<DeleteSchoolStandardMappingCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(
        DeleteSchoolStandardMappingCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(
            request.SchoolStandardMappingId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<Guid>.FailureResponse
            (
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolStandardMapping.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<Guid>.SuccessResponse
        (
            entity.SchoolStandardMappingId,
            messageHelper.DeletedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolStandardMapping.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}