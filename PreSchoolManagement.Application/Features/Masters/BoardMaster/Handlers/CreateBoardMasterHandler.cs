using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateBoardMasterHandler(
    IBoardMasterService service,
    IValidator<CreateBoardMasterCommand> validator,
    IMapper mapper,
    ICurrentUserService currentUser,
    IMessageHelper messageHelper)
: IRequestHandler<CreateBoardMasterCommand,ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateBoardMasterCommand request,CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request,cancellationToken);
        if(!validationResult.IsValid)
        {
            var message = string.Join(" | ",validationResult.Errors.Select(e => e.ErrorMessage));

            return ApiResponse<int>.FailureResponse(message,
            (int)HttpStatusCode.BadRequest);
        }

        var exists = await service.IsExistsAsync(request.BoardName ?? string.Empty,
        OperationType.Add,null,cancellationToken);

        if(exists)
        {
            return ApiResponse<int>.FailureResponse
            (
                messageHelper.AlreadyExistsEntity("Masters",EntityDescription.Board.ToString()),
                (int)HttpStatusCode.Conflict
            );
        }
        var entity = mapper.Map<BoardMaster>(request);

        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId;

        await service.AddAsync(entity, cancellationToken);
        return ApiResponse<int>.SuccessResponse
        (
            entity.BoardId,
            messageHelper.AddedEntity("Masters",EntityDescription.Board.ToString()),
            (int)HttpStatusCode.Created
        );
    }
}