using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class AcademicYearMasterProfile : Profile
{
    public AcademicYearMasterProfile()
    {
        CreateMap<AcademicYearMasterDto, AcademicYearMaster>();
        CreateMap<AcademicYearTranslationDto, AcademicYearTranslation>();
    }
}