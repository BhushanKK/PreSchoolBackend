using MediatR;
using SchoolAdmission.Domain.Dtos;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.Commands;
public class UpdateReligionMasterCommand : ReligionMasterDto, IRequest<ApiResponse<int>>;