using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdSchoolStandardMappingHandler(
    ISchoolStandardMappingService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetByIdSchoolStandardMappingQuery, ApiResponse<SchoolStandardMapping?>>
{
    public async Task<ApiResponse<SchoolStandardMapping?>> Handle(
        GetByIdSchoolStandardMappingQuery request,
        CancellationToken cancellationToken)
    {
        var schoolStandardMapping = await service.GetByIdAsync(
            request.SchoolStandardMappingId,
            cancellationToken);

        if (schoolStandardMapping is null)
        {
            return ApiResponse<SchoolStandardMapping?>.FailureResponse
            (
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolStandardMapping.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<SchoolStandardMapping?>.SuccessResponse
        (
            schoolStandardMapping,
            messageHelper.RetrievedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolStandardMapping.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}