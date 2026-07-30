using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class SchoolStandardMappingProfile : Profile
{
    public SchoolStandardMappingProfile()
    {
        CreateMap<SchoolStandardMappingDto,SchoolStandardMapping>();
    }
}