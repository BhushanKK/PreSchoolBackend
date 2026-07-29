using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteSchoolRegistrationCommand (Guid SchoolRegistrationId): IRequest<ApiResponse<Guid>>;