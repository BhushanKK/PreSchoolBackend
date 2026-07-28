using MediatR;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllDesignationMasterQuery(
    PaginationRequest Request)
    : IRequest<ApiResponse<PaginatedResult<DesignationMaster>>>;