using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteDesignationMasterCommand(int DesignationId)
    : IRequest<ApiResponse<int>>;
 