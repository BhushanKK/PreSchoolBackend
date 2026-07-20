using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class FinancialYearMasterProfile : Profile
{
    public FinancialYearMasterProfile()
    {
        CreateMap<FinancialYearMasterDto, FinancialYearMaster>();
        CreateMap<FinancialYearTranslationDto, FinancialYearTranslation>();
    }
}