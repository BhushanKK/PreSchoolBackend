using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteDivisionMasterCommand(int DivisionId) : IRequest<ApiResponse<int>>;