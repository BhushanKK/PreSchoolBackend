using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteChairmanMasterCommand(int ChairmanId) 
    : IRequest<ApiResponse<int>>;