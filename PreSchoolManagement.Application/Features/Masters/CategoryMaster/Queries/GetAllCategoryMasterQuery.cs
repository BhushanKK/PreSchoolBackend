using MediatR;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllCategoryMasterQuery(
    PaginationRequest Request)
    : IRequest<ApiResponse<PaginatedResult<CategoryMaster>>>;