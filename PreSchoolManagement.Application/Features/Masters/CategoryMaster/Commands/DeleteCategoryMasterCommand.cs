using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteCategoryMasterCommand(int CategoryId)
    : IRequest<ApiResponse<int>>;