using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetCasteDropdownQueryHandler(
    ICasteMasterService roleMasterService,
    IMessageHelper messageHelper)
    : IRequestHandler<GetCasteDropdownQuery, ApiResponse<List<CasteDropdownDto>>>
{
    public async Task<ApiResponse<List<CasteDropdownDto>>> Handle(
        GetCasteDropdownQuery request,
        CancellationToken cancellationToken)
    {
        var castes = await roleMasterService.GetActiveCastesAsync(cancellationToken);

        return ApiResponse<List<CasteDropdownDto>>.SuccessResponse
        (
            castes,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.Caste.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}