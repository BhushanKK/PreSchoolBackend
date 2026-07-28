using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetChairmanDropdownQueryHandler(
    IChairmanMasterService roleMasterService,
    IMessageHelper messageHelper)
    : IRequestHandler<GetChairmanDropdownQuery, ApiResponse<List<ChairmanDropdownDto>>>
{
    public async Task<ApiResponse<List<ChairmanDropdownDto>>> Handle(
        GetChairmanDropdownQuery request,
        CancellationToken cancellationToken)
    {
        var Chairmans = await roleMasterService.GetActiveChairmansAsync(cancellationToken);

        return ApiResponse<List<ChairmanDropdownDto>>.SuccessResponse
        (
            Chairmans,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.Chairman.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}