using System.Net;
using MediatR;
using Org.BouncyCastle.Ocsp;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdSchoolRegistrationHandler(
    ISchoolRegistrationService service,
    IMessageHelper messageHelper)
    : IRequestHandler<GetByIdSchoolRegistrationQuery, ApiResponse<SchoolRegistration>>
{
    public async Task<ApiResponse<SchoolRegistration>> Handle(
        GetByIdSchoolRegistrationQuery request,
        CancellationToken cancellationToken)
    {
        if(request.schoolRegistrationId == Guid.Empty)
        {
            return ApiResponse<SchoolRegistration>.FailureResponse(
                messageHelper.InvalidIdEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolRegistrations.ToString()),
                (int)HttpStatusCode.BadRequest);
        }

        var data = await service.GetByIdAsync(
            request.schoolRegistrationId,
            cancellationToken);
        
        if (data is null)
        {
            return ApiResponse<SchoolRegistration>.FailureResponse(
                messageHelper.NotFoundEntity(
                    LocaleEnums.Masters.ToString(),
                    EntityDescription.SchoolRegistrations.ToString()),
                (int)HttpStatusCode.NotFound);
                
        }

        return ApiResponse<SchoolRegistration>.SuccessResponse(
            data,
            messageHelper.RetrievedEntity(
                LocaleEnums.Masters.ToString(),
                EntityDescription.SchoolRegistrations.ToString()),
            (int)HttpStatusCode.OK);
            
    }
}
