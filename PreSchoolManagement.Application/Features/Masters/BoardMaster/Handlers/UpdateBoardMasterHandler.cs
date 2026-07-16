using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Application.Features.Handlers;

public class UpdateBoardMasterHandler(IBoardMasterService service,IValidator<UpdateBoardMasterCommand>validator,
IMapper mapper)
: IRequestHandler<UpdateBoardMasterCommand,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(UpdateBoardMasterCommand request,CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request,cancellationToken);

        if(!validationResult.IsValid)
        {
            var message = string.Join(" | ",validationResult.Errors.Select(e => e.ErrorMessage));

            return ApiResponse<int>.FailureResponse(message,(int)HttpStatusCode.BadRequest);
        }
        
        var entity = await service.GetByIdAsync(request.BoardId,cancellationToken);

        if(entity is null)
        {
            return ApiResponse<int>.FailureResponse(
                MessageHelper.NotFound(EntityDescription.Board.ToString()),
                (int)HttpStatusCode.NotFound);
            
        }

        var exists = await service.IsExistsAsync(request.BoardName ?? string.Empty,
        OperationType.Update,request.BoardId,cancellationToken);

        if(exists)
        {
            return ApiResponse<int>.FailureResponse(MessageHelper.AlreadyExists(EntityDescription.Board.ToString()),
            (int)HttpStatusCode.Conflict);
        }
        mapper.Map(request, entity);

        await service.UpdateAsync(entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.BoardId,MessageHelper.Updated(EntityDescription.Board.ToString()),
        (int)HttpStatusCode.OK);
    }
}