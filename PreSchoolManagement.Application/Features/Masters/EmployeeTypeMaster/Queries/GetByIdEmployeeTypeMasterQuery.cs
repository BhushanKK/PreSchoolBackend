using MediatR;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetByIdEmployeeTypeMasterQuery(int EmployeeTypeId)
    : IRequest<ApiResponse<EmployeeTypeMaster?>>;