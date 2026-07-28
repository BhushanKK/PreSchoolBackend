using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllCasteMasterHandler(
    ICasteMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<
        GetAllCasteMasterQuery,
        ApiResponse<PaginatedResult<CasteMasterQueryDto>>>
{
    public async Task<ApiResponse<PaginatedResult<CasteMasterQueryDto>>> Handle(
        GetAllCasteMasterQuery request,
        CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(request.Request,cancellationToken);

        return ApiResponse<PaginatedResult<CasteMasterQueryDto>>.SuccessResponse
        (
            result,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Caste.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}