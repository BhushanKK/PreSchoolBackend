using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteStandardMasterCommand(int StandardId) : IRequest<ApiResponse<int>>;