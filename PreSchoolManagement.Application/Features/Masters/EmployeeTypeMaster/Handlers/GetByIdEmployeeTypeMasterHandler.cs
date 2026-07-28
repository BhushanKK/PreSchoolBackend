using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdEmployeeTypeMasterHandler(
    IEmployeeTypeMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetByIdEmployeeTypeMasterQuery, ApiResponse<EmployeeTypeMaster?>>
{
    public async Task<ApiResponse<EmployeeTypeMaster?>> Handle(GetByIdEmployeeTypeMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(),EntityDescription.EmployeeType.ToString());
        
        if (request.EmployeeTypeId <= 0)
        {
            return ApiResponse<EmployeeTypeMaster?>.FailureResponse
            (
                messageHelper.InvalidId(EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.EmployeeTypeId,cancellationToken);

        if (data is null)
        {
            return ApiResponse<EmployeeTypeMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<EmployeeTypeMaster?>.SuccessResponse
        (
            data,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.EmployeeType.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}