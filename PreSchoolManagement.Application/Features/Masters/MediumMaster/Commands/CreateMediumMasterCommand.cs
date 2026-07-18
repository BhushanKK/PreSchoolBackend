using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public class CreateMediumMasterCommand : MediumMasterDto,
IRequest<ApiResponse<int>>;