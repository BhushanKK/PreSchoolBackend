using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class StateMasterProfile : Profile
{
    public StateMasterProfile()
    {
        CreateMap<StateTranslationDto, StateTranslation>();
        CreateMap<StateMasterDto ,StateMaster>();
    }
}