using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllDivisionMasterHandler(
    IDivisionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllDivisionMasterQuery, ApiResponse<PaginatedResult<DivisionMaster>>>
{
    public async Task<ApiResponse<PaginatedResult<DivisionMaster>>> Handle(
        GetAllDivisionMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Division.ToString());

        var Divisions = await service.GetAllAsync(request.Request, cancellationToken);
        
        if(Divisions is null)
        {
            return ApiResponse<PaginatedResult<DivisionMaster>>.SuccessResponse
            (
                Divisions,
                messageHelper.NotFoundEntity("Masters",EntityDescription.Division.ToString()),
                (int)HttpStatusCode.OK
            );    
        }
        return ApiResponse<PaginatedResult<DivisionMaster>>.SuccessResponse
        (
            Divisions,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}