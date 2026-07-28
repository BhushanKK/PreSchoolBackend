using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public record GetParentMenuQuery()
    : IRequest<ApiResponse<List<ParentMenuDto>>>;