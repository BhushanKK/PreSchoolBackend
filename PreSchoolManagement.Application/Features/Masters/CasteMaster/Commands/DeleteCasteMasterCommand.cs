using MediatR;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.CasteMasters.Commands;

public record DeleteCasteMasterCommand(int CasteId) : IRequest<ApiResponse<int>>;