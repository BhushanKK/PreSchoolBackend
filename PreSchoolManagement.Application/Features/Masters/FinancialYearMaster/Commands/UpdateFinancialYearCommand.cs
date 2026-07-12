using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public class UpdateFinancialYearMasterCommand 
    : FinancialYearMasterDto, IRequest<ApiResponse<int>>;