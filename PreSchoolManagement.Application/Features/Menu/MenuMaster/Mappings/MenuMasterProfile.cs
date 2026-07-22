using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class MenuMasterProfile : Profile
{
    public MenuMasterProfile()
    {
        CreateMap<MenuMaster, MenuMasterDto>()
            .ReverseMap();

        CreateMap<MenuTranslation, MenuTranslationDto>()
            .ReverseMap();

        CreateMap<MenuMaster, MenuMasterQueryDto>()
            .ReverseMap();
    }
}