using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdAcademicYearMasterHandler(
    IAcademicYearMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetByIdAcademicYearMasterQuery, ApiResponse<AcademicYearMaster?>>
{
    public async Task<ApiResponse<AcademicYearMaster?>> Handle(
        GetByIdAcademicYearMasterQuery request,
        CancellationToken cancellationToken)
    {
        var academicYear = await service.GetByIdAsync(request.AcademicYearId,cancellationToken);

        if (academicYear is null)
        {
            return ApiResponse<AcademicYearMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<AcademicYearMaster?>.SuccessResponse
        (
            academicYear,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.AcademicYear.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}