using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteAcademicYearMasterHandler(
    IAcademicYearMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<DeleteAcademicYearMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(
        DeleteAcademicYearMasterCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(
            request.academicYearId,
            cancellationToken);

        if (entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.AcademicYear.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(entity, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.AcademicYearId,
            messageHelper.DeletedEntity(LocaleEnums.Masters.ToString(),EntityDescription.AcademicYear.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}