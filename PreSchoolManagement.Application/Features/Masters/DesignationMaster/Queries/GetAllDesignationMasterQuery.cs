using MediatR;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllDesignationMasterQuery(bool filter = false)
    : IRequest<ApiResponse<List<DesignationMaster>>>;