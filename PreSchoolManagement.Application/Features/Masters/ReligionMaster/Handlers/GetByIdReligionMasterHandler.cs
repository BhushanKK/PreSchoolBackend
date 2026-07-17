using MediatR;
using System.Net;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;
namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdReligionMasterHandler(
    IReligionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetByIdReligionMasterQuery, ApiResponse<ReligionMaster?>>
{
    public async Task<ApiResponse<ReligionMaster?>> Handle(GetByIdReligionMasterQuery request, CancellationToken cancellationToken)
    {
        localization.Get("Masters", EntityDescription.Religion.ToString());

        if (request.ReligionId <= 0)
        {
            return ApiResponse<ReligionMaster?>.FailureResponse
            (
                messageHelper.InvalidIdEntity("Masters", EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.BadRequest
            );
        }

        var data = await service.GetByIdAsync(request.ReligionId, cancellationToken);

        if (data is null)
        {
            return ApiResponse<ReligionMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters", EntityDescription.Religion.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<ReligionMaster?>.SuccessResponse
        (
            data, 
            messageHelper.RetrievedEntity("Masters", EntityDescription.Religion.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}