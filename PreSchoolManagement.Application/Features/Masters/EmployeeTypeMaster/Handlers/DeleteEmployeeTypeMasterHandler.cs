using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Masters.Handlers;

public class DeleteEmployeeTypeMasterHandler(
    IEmployeeTypeMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<DeleteEmployeeTypeMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteEmployeeTypeMasterCommand request, CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.EmployeeType.ToString());

        if (request.EmployeeTypeId <= 0)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters", EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var existing = await service.GetByIdAsync(request.EmployeeTypeId, cancellationToken);
        if (existing is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.EmployeeType.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        await service.DeleteAsync(existing, cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            request.EmployeeTypeId,
            messageHelper.DeletedEntity("Masters", EntityDescription.EmployeeType.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}