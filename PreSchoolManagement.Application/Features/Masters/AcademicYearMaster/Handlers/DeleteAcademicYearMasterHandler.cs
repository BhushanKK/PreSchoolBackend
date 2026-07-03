using MediatR;
using System.Net;
using PreSchoolManagement.Shared.Utils;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Application.Features.Commands;

namespace SchoolAdmission.Application.Features.Masters.Handlers;

public class DeleteAcademicYearMasterHandler(IAcademicYearMasterService service) : IRequestHandler<DeleteAcademicYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteAcademicYearMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.academicYearId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.InvalidId(EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.academicYearId, cancellationToken);

        if (existing is null)
            return ApiResponse<int>.FailureResponse
            (
                MessageHelper.NotFound(EntityDescription.AcademicYear.ToString()), 
                (int)HttpStatusCode.NotFound
            );

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.academicYearId, 
            MessageHelper.Deleted(EntityDescription.AcademicYear.ToString()), 
            (int)HttpStatusCode.OK
        );
    }
}
