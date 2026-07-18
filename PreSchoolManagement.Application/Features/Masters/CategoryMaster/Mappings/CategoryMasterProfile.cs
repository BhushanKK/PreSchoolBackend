using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class CategoryMasterProfile : Profile
{
    public CategoryMasterProfile()
    {
        CreateMap<CategoryMasterDto, CategoryMaster>();
        CreateMap<CategoryTranslationDto, CategoryTranslation>();
    }
}