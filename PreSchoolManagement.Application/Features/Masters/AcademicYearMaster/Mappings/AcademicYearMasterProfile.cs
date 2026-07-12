using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class AcademicYearMasterProfile : Profile
{
    public AcademicYearMasterProfile()
    {
        CreateMap<AcademicYearMasterDto, AcademicYearMaster>()
            .ForMember(d => d.AcademicYearId, opt => opt.MapFrom(s => s.AcademicYearId))            
            .ForMember(d => d.AcademicYearName, opt => opt.MapFrom(s => s.AcademicYearName ?? string.Empty));
    }
}