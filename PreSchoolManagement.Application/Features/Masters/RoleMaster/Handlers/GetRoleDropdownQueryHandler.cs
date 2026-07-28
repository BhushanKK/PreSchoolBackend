using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetRoleDropdownQueryHandler(
    IRoleMasterService roleMasterService,
    IMessageHelper messageHelper)
    : IRequestHandler<GetRoleDropdownQuery, ApiResponse<List<RoleDropdownDto>>>
{
    public async Task<ApiResponse<List<RoleDropdownDto>>> Handle(
        GetRoleDropdownQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await roleMasterService.GetActiveRolesAsync(cancellationToken);

        return ApiResponse<List<RoleDropdownDto>>.SuccessResponse(
            roles,
            messageHelper.RetrievedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.Role.ToString()),
            (int)HttpStatusCode.OK);
    }
}