using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteMenuMasterCommand(int MenuId)
    : IRequest<ApiResponse<int>>;