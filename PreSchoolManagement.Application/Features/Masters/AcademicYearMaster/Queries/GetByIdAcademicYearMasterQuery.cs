using MediatR;
using SchoolAdmission.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Queries;

public sealed record GetByIdAcademicYearMasterQuery(int AcademicYearId) : IRequest<ApiResponse<AcademicYearMaster?>>;
