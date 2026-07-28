using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdSchoolDetailsMasterHandler(
    ISchoolDetailsMasterService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetByIdSchoolDetailsMasterQuery, ApiResponse<SchoolDetailsMaster>>
{
    public async Task<ApiResponse<SchoolDetailsMaster>> Handle(
        GetByIdSchoolDetailsMasterQuery request,
        CancellationToken cancellationToken)
    {
        if (request.schoolDetailsId == Guid.Empty)
        {
            return ApiResponse<SchoolDetailsMaster>.FailureResponse(
                messageHelper.InvalidIdEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolDetails.ToString()),
                (int)HttpStatusCode.BadRequest);
        }

        var data = await service.GetByIdAsync(
            request.schoolDetailsId,
            cancellationToken);

        if (data is null)
        {
            return ApiResponse<SchoolDetailsMaster>.FailureResponse(
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolDetails.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<SchoolDetailsMaster>.SuccessResponse(
            data,
            messageHelper.RetrievedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolDetails.ToString()),
            (int)HttpStatusCode.OK);
    }
}