using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteAcademicYearMasterHandler(
    IAcademicYearMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localizer) 
: IRequestHandler<DeleteAcademicYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteAcademicYearMasterCommand request, CancellationToken cancellationToken)
    {
        localizer.Get("Masters", EntityDescription.AcademicYear.ToString());

        if (request.academicYearId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters", EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }
        
        var existing = await service.GetByIdAsync(request.academicYearId, cancellationToken);
        
        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.academicYearId, 
            messageHelper.DeletedEntity("Masters", EntityDescription.AcademicYear.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}
