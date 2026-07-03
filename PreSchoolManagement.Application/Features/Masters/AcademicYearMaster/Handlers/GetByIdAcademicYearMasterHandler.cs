using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolAdmission.Application.Features.Queries;

namespace SchoolAdmission.Application.Features.Handlers;

public class GetByIdAcademicYearMasterHandler(IAcademicYearMasterService service) : IRequestHandler<GetByIdAcademicYearMasterQuery, ApiResponse<AcademicYearMaster?>>
{
    public async Task<ApiResponse<AcademicYearMaster?>> Handle(GetByIdAcademicYearMasterQuery request, CancellationToken cancellationToken)
    {
        if (request.AcademicYearId <= 0)
        {
            return ApiResponse<AcademicYearMaster?>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.AcademicYearId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<AcademicYearMaster?>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<AcademicYearMaster?>.SuccessResponse
        (
            data, 
            MessageHelper.Retrieved(EntityDescription.AcademicYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
