using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteSectionMasterCommand(int SectionId) : IRequest<ApiResponse<int>>;