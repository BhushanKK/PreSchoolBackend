using AutoMapper;
using SchoolAdmission.Application.Features.CasteMasters.Commands;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Masters.CasteMaster.Mappings;

public class CasteMasterProfile : Profile
{
    public CasteMasterProfile()
    {
        CreateMap<CreateCasteMasterCommand, SchoolManagement.Domain.Entities.CasteMaster>()
            .ForMember(d => d.CasteID, opt => opt.MapFrom(s => s.CasteId))
            .ForMember(d => d.CategoryID, opt => opt.MapFrom(s => s.CategoryId ?? 0))
            .ForMember(d => d.CasteName, opt => opt.MapFrom(s => s.Caste ?? string.Empty));

        CreateMap<UpdateCasteMasterCommand, SchoolManagement.Domain.Entities.CasteMaster>()
            .ForMember(d => d.CasteID, opt => opt.MapFrom(s => s.CasteId))
            .ForMember(d => d.CategoryID, opt => opt.MapFrom(s => s.CategoryId ?? 0))
            .ForMember(d => d.CasteName, opt => opt.MapFrom(s => s.Caste ?? string.Empty));
    }
}
