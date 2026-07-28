using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public record GetAllMenuMasterQuery(
    PaginationRequest Request)
    : IRequest<ApiResponse<PaginatedResult<MenuMasterQueryDto>>>;