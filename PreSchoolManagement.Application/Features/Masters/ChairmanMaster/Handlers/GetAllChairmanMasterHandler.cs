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

public class GetAllChairmanMasterHandler(
    IChairmanMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<
        GetAllChairmanMasterQuery,
        ApiResponse<PaginatedResult<ChairmanMasterQueryDto>>>
{
    public async Task<ApiResponse<PaginatedResult<ChairmanMasterQueryDto>>> Handle(
        GetAllChairmanMasterQuery request,
        CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(request.Request,cancellationToken);

        return ApiResponse<PaginatedResult<ChairmanMasterQueryDto>>.SuccessResponse
        (
            result,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Chairman.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}