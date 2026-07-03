using MediatR;
using SchoolAdmission.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Queries;

public sealed record GetByIdReligionMasterQuery(int ReligionId) : IRequest<ApiResponse<ReligionMaster?>>;