using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdAcademicYearMasterHandler(
    IAcademicYearMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization) 
    : IRequestHandler<GetByIdAcademicYearMasterQuery, ApiResponse<AcademicYearMaster?>>
{
    public async Task<ApiResponse<AcademicYearMaster?>> Handle(GetByIdAcademicYearMasterQuery request, CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.AcademicYear.ToString());
        
        if (request.AcademicYearId <= 0)
        {
            return ApiResponse<AcademicYearMaster?>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters",EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.AcademicYearId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<AcademicYearMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<AcademicYearMaster?>.SuccessResponse
        (
            data, 
            messageHelper.RetrievedEntity("Masters",EntityDescription.AcademicYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
