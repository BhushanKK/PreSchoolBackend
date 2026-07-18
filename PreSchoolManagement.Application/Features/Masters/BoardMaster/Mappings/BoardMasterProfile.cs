using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;
public class BoardMasterProfile : Profile
{
    public  BoardMasterProfile()
    {
        CreateMap<BoardMasterDto, BoardMaster>();
        CreateMap<BoardTranslationDto, BoardTranslation>();
    }
}