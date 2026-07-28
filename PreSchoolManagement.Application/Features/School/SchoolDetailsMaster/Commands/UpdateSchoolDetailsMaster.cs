using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Commands;

public class UpdateSchoolDetailsMasterCommand :
SchoolDetailsDto,IRequest<ApiResponse<Guid>>;