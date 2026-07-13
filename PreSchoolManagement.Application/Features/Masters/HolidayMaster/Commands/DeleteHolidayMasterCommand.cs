using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteHolidayMasterCommand(int HolidayId) : IRequest<ApiResponse<int>>;