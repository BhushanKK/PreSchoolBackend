using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Handlers;

public class DeleteBoardMasterHandler(
    IBoardMasterService service,
    IMessageHelper messageHelper,
    ILocalizationService localization)
: IRequestHandler<DeleteBoardMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(DeleteBoardMasterCommand request,
    CancellationToken cancellationToken)
    {
        localization.Get("Masters",EntityDescription.Board.ToString());

        var entity = await service.GetByIdAsync(request.BoardId,cancellationToken);
        
        if(entity is null)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.NotFoundEntity("Masters",EntityDescription.Board.ToString()),
                (int)HttpStatusCode.NotFound
            );
        }
        await service.DeleteAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse
        (
            entity.BoardId,
            messageHelper.DeletedEntity("Masters",EntityDescription.Board.ToString()
        ),
        (int)HttpStatusCode.OK);
    }
}