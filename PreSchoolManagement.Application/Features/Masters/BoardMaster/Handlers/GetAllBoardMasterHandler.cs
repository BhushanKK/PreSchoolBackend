using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetAllBoardMasterHandler(IBoardMasterService service)
: IRequestHandler<GetAllBoardMasterQuery, ApiResponse<List<BoardMaster>>>
{
    public async Task<ApiResponse<List<BoardMaster>>> Handle( GetAllBoardMasterQuery request,CancellationToken cancellationToken)
    {
        var boards = await service.GetAllAsync(cancellationToken);
         
        return ApiResponse<List<BoardMaster>>.SuccessResponse(boards,
        MessageHelper.Retrieved(EntityDescription.Board.ToString()),
        (int)HttpStatusCode.OK);
    }
}