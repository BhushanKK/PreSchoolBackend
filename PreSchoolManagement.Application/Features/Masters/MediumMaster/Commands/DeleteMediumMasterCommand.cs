using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteMediumMasterCommand(int MediumId)
: IRequest<ApiResponse<int>>;