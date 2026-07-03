using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class FinancialYearMasterProfile : Profile
{
    public FinancialYearMasterProfile()
    {
        CreateMap<FinancialYearMasterDto, FinancialYearMaster>()
            .ForMember(d => d.FinancialYearId, opt => opt.MapFrom(s => s.FinancialYearId))            
            .ForMember(d => d.FinancialYearName, opt => opt.MapFrom(s => s.FinancialYearName ?? string.Empty));
    }
}