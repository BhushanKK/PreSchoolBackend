using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public record GetRolePermissionDetailsQuery(int roleId)
    : IRequest<ApiResponse<List<UserPermissionDto>>>;