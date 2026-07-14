using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public class CreateDesignationMasterCommand 
    : DesignationMasterDto,IRequest<ApiResponse<int>>;