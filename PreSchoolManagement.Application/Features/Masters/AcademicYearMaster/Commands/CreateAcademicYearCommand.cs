using MediatR;
using SchoolAdmission.Domain.Dtos;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.Commands;

public class CreateAcademicYearMasterCommand : AcademicYearMasterDto, IRequest<ApiResponse<int>>;