using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;
public class BoardMasterProfile : Profile
{
    public  BoardMasterProfile()
    {
        CreateMap<BoardMasterDto, BoardMaster>()
            .ForMember(d => d.BoardId, opt => opt.MapFrom(s => s.BoardId))
            .ForMember(d => d.BoardName,opt => opt.MapFrom(s => s.BoardName ?? string.Empty));
    }
}