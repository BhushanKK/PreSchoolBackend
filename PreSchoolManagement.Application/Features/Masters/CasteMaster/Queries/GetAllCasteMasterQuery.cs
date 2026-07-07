using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public sealed record GetAllCasteMasterQuery 
    : IRequest<ApiResponse<List<CasteMasterQueryDto>>>;