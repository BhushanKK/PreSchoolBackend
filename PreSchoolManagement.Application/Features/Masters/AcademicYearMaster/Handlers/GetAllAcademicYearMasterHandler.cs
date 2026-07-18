using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllAcademicYearMasterHandler(
    IAcademicYearMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localizer)
: IRequestHandler<GetAllAcademicYearMasterQuery, ApiResponse<List<AcademicYearMaster>>>
{
    public async Task<ApiResponse<List<AcademicYearMaster>>> Handle(GetAllAcademicYearMasterQuery request, CancellationToken cancellationToken)
    {
        localizer.Get("Masters", EntityDescription.AcademicYear.ToString());
        var data = await service.GetAllAsync(request.filter, cancellationToken);

        if (data != null)
        {
            return ApiResponse<List<AcademicYearMaster>>.SuccessResponse
            (
                data,
                messageHelper.RetrievedEntity("Masters", EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<AcademicYearMaster>>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
