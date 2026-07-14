using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteEmployeeTypeMasterCommand(int EmployeeTypeId)
    :IRequest<ApiResponse<int>>;