using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllBoardMasterHandler(
    IBoardMasterService service,
    IMessageHelper messageHelper)
: IRequestHandler<GetAllBoardMasterQuery, ApiResponse<List<BoardMaster>>>
{
    public async Task<ApiResponse<List<BoardMaster>>> Handle(GetAllBoardMasterQuery request,CancellationToken cancellationToken)
    {
        var boards = await service.GetAllAsync(request.filter,cancellationToken);
         
        return ApiResponse<List<BoardMaster>>.SuccessResponse
        (
            boards,
            messageHelper.RetrievedEntity(LocaleEnums.Masters.ToString(),EntityDescription.Board.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}