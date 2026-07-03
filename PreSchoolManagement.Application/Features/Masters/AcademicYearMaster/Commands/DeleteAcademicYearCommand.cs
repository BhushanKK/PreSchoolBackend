using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteAcademicYearMasterCommand(int academicYearId)  
    : IRequest<ApiResponse<int>>;