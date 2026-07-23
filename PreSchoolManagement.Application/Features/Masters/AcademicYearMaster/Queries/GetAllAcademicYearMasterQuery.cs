using MediatR;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllAcademicYearMasterQuery(
    PaginationRequest Request)
    : IRequest<ApiResponse<PaginatedResult<AcademicYearMaster>>>;