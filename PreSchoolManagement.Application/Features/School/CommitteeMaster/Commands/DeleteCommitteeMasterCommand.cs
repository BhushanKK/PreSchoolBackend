using MediatR;
using PreSchoolManagement.Domain.ResponseModels;
namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteCommitteeMasterCommand(Guid CommitteeId):IRequest<ApiResponse<Guid>>;