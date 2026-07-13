using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public record GetRoleMenuPermissionQuery(int RoleId)
    : IRequest<ApiResponse<List<RoleMenuPermissionDto>>>;