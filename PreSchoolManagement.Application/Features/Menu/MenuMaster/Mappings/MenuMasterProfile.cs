using AutoMapper;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class MenuMasterProfile : Profile
{
    public MenuMasterProfile()
    {
        CreateMap<MenuMasterDto, MenuMaster>()
            .ForMember(d => d.MenuId, opt => opt.MapFrom(s => s.MenuId))
            .ReverseMap();
    }
}