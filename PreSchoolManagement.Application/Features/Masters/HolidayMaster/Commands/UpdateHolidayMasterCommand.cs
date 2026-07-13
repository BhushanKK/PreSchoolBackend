using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public class UpdateHolidayMasterCommand : HolidayMasterDto, IRequest<ApiResponse<int>>;