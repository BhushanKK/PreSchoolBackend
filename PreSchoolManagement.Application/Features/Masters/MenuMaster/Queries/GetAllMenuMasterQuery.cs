using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public record GetAllMenuMasterQuery()
    : IRequest<ApiResponse<List<MenuMasterQueryDto>>>;