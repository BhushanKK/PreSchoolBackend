using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public class SaveRoleMenuPermissionCommand :
SaveRoleMenuPermissionDto, IRequest<ApiResponse<bool>>;
