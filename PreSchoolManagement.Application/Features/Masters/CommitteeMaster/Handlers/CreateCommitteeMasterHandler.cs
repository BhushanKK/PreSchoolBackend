using MediatR;
using AutoMapper;
using System.Net;
using FluentValidation;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Utils;
using SchoolManagement.Domain.Entities;


namespace PreSchoolManagement.Application.Features.Handlers;

public class CreateCommitteeMasterHandler(ICommitteeMasterService service,IValidator<CreateCommitteeMasterCommand>validator,
IMapper mapper,ICurrentUserService currentUser)
: IRequestHandler<CreateCommitteeMasterCommand, ApiResponse<int>>
{
    public async Task<ApiResponse<int>> Handle(CreateCommitteeMasterCommand request,CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request,cancellationToken);
        if(!validationResult.IsValid)
        {
            var message = string.Join("|", validationResult.Errors.Select(e => e.ErrorMessage));
            return ApiResponse<int>.FailureResponse(message,(int)HttpStatusCode.BadRequest);

        }

        var exists = await service.IsExistsAsync(request.CommitteeName ?? string.Empty,OperationType.Add,null,cancellationToken);

        if(exists)
        {
            return ApiResponse<int>.FailureResponse(MessageHelper.AlreadyExists(EntityDescription.committee.ToString()),
            (int)HttpStatusCode.Conflict);
        }

        var entity = mapper.Map<CommitteeMaster>(request);

        entity.EntryDate = DateTime.UtcNow;
        entity.EntryBy = currentUser.UserId;

        await service.AddAsync (entity,cancellationToken);

        return ApiResponse<int>.SuccessResponse(entity.CommitteeId,MessageHelper.Added(EntityDescription.committee.ToString()),
        (int)HttpStatusCode.Created
        );
    }
}