using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class SchoolDetailsMasterProfile : Profile
{
    public SchoolDetailsMasterProfile()
    {
        CreateMap<SchoolDetailsDto, SchoolDetailsMaster>();

    }
}