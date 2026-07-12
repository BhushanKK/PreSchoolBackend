using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteDistrictMasterCommand(int DistrictId)
    :IRequest<ApiResponse<int>>;
    