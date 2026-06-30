using MediatR;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.CasteMasters.Commands;

public class DeleteCasteMasterCommand : IRequest<ApiResponse<int>>
{
    public int CasteId { get; set; }
}
