using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetMenuMasterByIdQuery(int MenuId)
    : IRequest<ApiResponse<MenuMasterDto?>>;
    