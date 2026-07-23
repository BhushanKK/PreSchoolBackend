using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllEmployeeTypeMasterHandler(
    IEmployeeTypeMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllEmployeeTypeMasterQuery, ApiResponse<List<EmployeeTypeMaster>>>
{
    public async Task<ApiResponse<List<EmployeeTypeMaster>>> Handle(GetAllEmployeeTypeMasterQuery request,
     CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(), EntityDescription.EmployeeType.ToString());

        var data = await service.GetAllAsync(request.filter, cancellationToken);

        if (data != null)
        {
            return ApiResponse<List<EmployeeTypeMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<EmployeeTypeMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(), EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}