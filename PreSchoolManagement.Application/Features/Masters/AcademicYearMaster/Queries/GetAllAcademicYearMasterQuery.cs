using MediatR;
using SchoolAdmission.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Queries;

public sealed record GetAllAcademicYearMasterQuery : IRequest<ApiResponse<List<AcademicYearMaster>>>;