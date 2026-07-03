using System.Net;
using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

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
