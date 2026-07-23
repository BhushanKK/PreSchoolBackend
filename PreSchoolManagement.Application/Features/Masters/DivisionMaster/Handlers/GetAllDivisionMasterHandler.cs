using MediatR;
using System.Net;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Localization;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllDivisionMasterHandler(
    IDivisionMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetAllDivisionMasterQuery, ApiResponse<List<DivisionMaster>>>
{
    public async Task<ApiResponse<List<DivisionMaster>>> Handle(
        GetAllDivisionMasterQuery request,
        CancellationToken cancellationToken)
    {
        localization.Get(LocaleEnums.Masters.ToString(),EntityDescription.Division.ToString());

        var Divisions = await service.GetAllAsync(request.filter, cancellationToken);
        
        if(Divisions is null)
        {
            return ApiResponse<List<DivisionMaster>>.SuccessResponse
            (
                Divisions,
                messageHelper.NotFoundEntity(LocaleEnums.Masters.ToString(),EntityDescription.Division.ToString()),
                (int)HttpStatusCode.OK
            );    
        }
        return ApiResponse<List<DivisionMaster>>.SuccessResponse
        (
            Divisions,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Division.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}