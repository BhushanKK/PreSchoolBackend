using System.Net;
using MediatR;
using PreSchoolManagement.Shared.Utils;
using SchoolAdmission.Application.Features.Queries;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Handlers;

public class GetAllAcademicYearMasterHandler(IAcademicYearMasterService service) : IRequestHandler<GetAllAcademicYearMasterQuery, ApiResponse<List<AcademicYearMaster>>>
{
    public async Task<ApiResponse<List<AcademicYearMaster>>> Handle(GetAllAcademicYearMasterQuery request, CancellationToken cancellationToken)
    {
        var data = await service.GetAllAsync(cancellationToken);
        if(data!=null)
        {
            return ApiResponse<List<AcademicYearMaster>>.SuccessResponse
            (
                data, 
                MessageHelper.Retrieved(EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
        else
        {
            return ApiResponse<List<AcademicYearMaster>>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.OK
            );
        }
    }
}
