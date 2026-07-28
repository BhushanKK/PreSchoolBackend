using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteSchoolDetailsMasterCommand
(Guid SchoolDetailsId) : IRequest<ApiResponse<Guid>>;