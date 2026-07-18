using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteBoardMasterCommand(int BoardId)
: IRequest<ApiResponse<int>>;