using MediatR;
using PreSchoolManagement.Domain.ResponseModels;
namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteCommitteeMasterCommand(int CommitteeId):IRequest<ApiResponse<int>>;