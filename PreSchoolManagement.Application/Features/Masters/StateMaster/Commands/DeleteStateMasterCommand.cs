using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteStateMasterCommand(int StateId)
    : IRequest<ApiResponse<int>>;