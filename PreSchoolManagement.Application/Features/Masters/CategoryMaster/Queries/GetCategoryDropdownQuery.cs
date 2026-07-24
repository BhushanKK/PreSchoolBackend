using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public record GetCategoryDropdownQuery
    : IRequest<ApiResponse<List<CategoryDropdownDto>>>;