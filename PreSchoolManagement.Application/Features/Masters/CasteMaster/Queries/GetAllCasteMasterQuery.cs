using MediatR;
using SchoolAdmission.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.CasteMasters.Queries;

public class GetAllCasteMasterQuery : IRequest<ApiResponse<List<CasteMaster>>>;