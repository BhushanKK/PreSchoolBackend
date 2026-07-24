using MediatR;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllDistrictMasterQuery(PaginationRequest Request)
    : IRequest<ApiResponse<PaginatedResult<DistrictMasterQueryDto>>>;