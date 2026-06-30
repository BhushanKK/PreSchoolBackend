using MediatR;
using SchoolAdmission.Domain.ResponseModels;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.CasteMasters.Queries;

public class GetByIdCasteMasterQuery : IRequest<ApiResponse<CasteMaster?>>
{
    public int CasteId { get; set; }
}
