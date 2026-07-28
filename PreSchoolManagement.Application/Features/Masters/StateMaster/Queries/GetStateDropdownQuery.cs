using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Queries;

public record GetStateDropdownQuery
    : IRequest<ApiResponse<List<StateDropdownDto>>>;