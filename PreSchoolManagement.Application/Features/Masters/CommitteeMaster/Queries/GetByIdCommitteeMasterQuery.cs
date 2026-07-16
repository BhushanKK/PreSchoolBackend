using MediatR;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetByIdCommitteeMasterQuery(int CommiteeId)
: IRequest<ApiResponse<CommitteeMaster?>>;