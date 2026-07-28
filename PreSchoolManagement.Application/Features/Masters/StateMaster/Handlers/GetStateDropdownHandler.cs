using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetStateDropdownQueryHandler(
    IStateMasterService stateMasterService,
    IMessageHelper messageHelper)
    : IRequestHandler<GetStateDropdownQuery, ApiResponse<List<StateDropdownDto>>>
{
    public async Task<ApiResponse<List<StateDropdownDto>>> Handle(
        GetStateDropdownQuery request,
        CancellationToken cancellationToken)
    {
        var states = await stateMasterService.GetActiveStateAsync(cancellationToken);

        return ApiResponse<List<StateDropdownDto>>.SuccessResponse(
            states,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.State.ToString()),
            (int)HttpStatusCode.OK);
    }
}