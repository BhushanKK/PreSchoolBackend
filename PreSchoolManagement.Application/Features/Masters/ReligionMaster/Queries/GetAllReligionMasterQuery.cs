using MediatR;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllReligionMasterQuery(PaginationRequest Request)
    : IRequest<ApiResponse<PaginatedResult<ReligionMaster>>>;
