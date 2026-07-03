using MediatR;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.Commands;
public record DeleteReligionMasterCommand(int ReligionId) : IRequest<ApiResponse<int>>;