using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class SchoolRegistrationMasterProfile : Profile
{
    public SchoolRegistrationMasterProfile()
    {
        CreateMap<SchoolRegistrationDto, SchoolRegistration>();
    }
}