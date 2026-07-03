using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteFinancialYearMasterCommand(int FinancialYearId)  
    : IRequest<ApiResponse<int>>;