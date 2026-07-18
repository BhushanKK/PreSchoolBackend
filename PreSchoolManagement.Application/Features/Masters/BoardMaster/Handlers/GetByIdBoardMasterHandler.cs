using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class GetByIdBoardMasterHandler(
    IBoardMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
    : IRequestHandler<GetByIdBoardMasterQuery,ApiResponse<BoardMaster?>>
{
    public async Task<ApiResponse<BoardMaster?>> Handle(
        GetByIdBoardMasterQuery request,CancellationToken cancellationToken)

    {
        

        var board = await service.GetByIdAsync(request.BoardId,cancellationToken);

        if(board is null)
        {
            return ApiResponse<BoardMaster?>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Board.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }

        return ApiResponse<BoardMaster?>.SuccessResponse
        (
            board,
            messageHelper.RetrievedEntity("Masters",EntityDescription.Board.ToString()),
            (int)HttpStatusCode.OK
        );
    }
}