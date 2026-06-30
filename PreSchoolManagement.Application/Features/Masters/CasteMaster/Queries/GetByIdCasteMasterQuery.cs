using MediatR;
using SchoolAdmission.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.CasteMasters.Queries;

public record GetByIdCasteMasterQuery(int CasteId) : IRequest<ApiResponse<CasteMaster?>>;
