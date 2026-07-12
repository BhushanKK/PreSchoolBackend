using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllCasteMasterQuery(bool filter = false)
    : IRequest<ApiResponse<List<CasteMasterQueryDto>>>;