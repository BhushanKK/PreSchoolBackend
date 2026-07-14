using MediatR;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllEmployeeTypeMasterQuery(bool filter =false)
    : IRequest<ApiResponse<List<EmployeeTypeMaster>>>;