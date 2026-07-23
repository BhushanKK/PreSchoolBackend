using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllAcademicYearMasterHandler(
    IAcademicYearMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetAllAcademicYearMasterQuery, ApiResponse<List<AcademicYearMaster>>>
{
    public async Task<ApiResponse<List<AcademicYearMaster>>> Handle(
        GetAllAcademicYearMasterQuery request,
        CancellationToken cancellationToken)
    {
        var academicYears = await service.GetAllAsync(request.filter, cancellationToken);

        return ApiResponse<List<AcademicYearMaster>>.SuccessResponse
        (
            academicYears,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(), EntityDescription.AcademicYear.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}