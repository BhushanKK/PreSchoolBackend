using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public record GetMenuMasterByIdQuery(int MenuId)
    : IRequest<ApiResponse<MenuMasterDto?>>;