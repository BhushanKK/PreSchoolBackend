using MediatR;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.Commands;

public record DeleteAcademicYearMasterCommand(int academicYearId) : IRequest<ApiResponse<int>>;