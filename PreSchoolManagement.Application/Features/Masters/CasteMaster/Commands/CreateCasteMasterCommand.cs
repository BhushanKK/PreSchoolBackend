using MediatR;
using SchoolAdmission.Domain.Dtos;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.CasteMasters.Commands;

public class CreateCasteMasterCommand : CasteMasterCommandDto, IRequest<ApiResponse<int>>;