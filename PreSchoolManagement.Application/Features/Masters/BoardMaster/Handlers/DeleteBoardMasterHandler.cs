using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteBoardMasterHandler(IBoardMasterService service)
: IRequestHandler<DeleteBoardMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteBoardMasterCommand request,
    CancellationToken cancellationToken)
    {
        var entity = await service.GetByIdAsync(request.BoardId,cancellationToken);
        
        if(entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Board.ToString()),
                (int)HttpStatusCode.NotFound);
        }
        await service.DeleteAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.BoardId,MessageHelper.Deleted(EntityDescription.Board.ToString()),
        (int)HttpStatusCode.OK);
    }
}