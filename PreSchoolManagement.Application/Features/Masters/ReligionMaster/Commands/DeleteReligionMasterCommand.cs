using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;
public record DeleteReligionMasterCommand(int ReligionId) 
    : IRequest<ApiResponse<int>>;