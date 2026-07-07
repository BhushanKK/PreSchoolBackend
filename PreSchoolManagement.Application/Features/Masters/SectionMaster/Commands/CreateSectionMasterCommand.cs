using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public class CreateSectionMasterCommand : SectionMasterDto, IRequest<ApiResponse<int>>;