using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllSchoolStandardMappingHandler(
    ISchoolStandardMappingService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllSchoolStandardMappingQuery,
      ApiResponse<PaginatedResult<SchoolStandardMapping>>>
{
    public async Task<ApiResponse<PaginatedResult<SchoolStandardMapping>>> Handle(
        GetAllSchoolStandardMappingQuery request,
        CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(request.Request, cancellationToken);

        return ApiResponse<PaginatedResult<SchoolStandardMapping>>.SuccessResponse
        (
            result,
            messageHelper.RetrievedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolStandardMapping.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}