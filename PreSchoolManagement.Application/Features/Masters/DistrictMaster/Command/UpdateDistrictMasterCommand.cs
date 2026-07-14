using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public class UpdateDistrictMasterCommand
    : DistrictMasterDto, IRequest<ApiResponse<int>>;