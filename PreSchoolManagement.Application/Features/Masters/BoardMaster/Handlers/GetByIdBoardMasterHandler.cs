using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdBoardMasterHandler(IBoardMasterService service)
: IRequestHandler<GetByIdBoardMasterQuery,ApiResponse<BoardMaster?>>
{
    public async Task<ApiResponse<BoardMaster?>> Handle(
        GetByIdBoardMasterQuery request,CancellationToken cancellationToken)

    {
        var board = await service.GetByIdAsync(request.BoardId,cancellationToken);

        if(board is null)
        {
            return ApiResponse<BoardMaster?>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Board.ToString()),
                (int)HttpStatusCode.NotFound);
        }

        return ApiResponse<BoardMaster?>.SuccessResponse(
            board,
            MessageHelper.Retrieved(EntityDescription.Board.ToString()),
            (int)HttpStatusCode.OK);
    }
}