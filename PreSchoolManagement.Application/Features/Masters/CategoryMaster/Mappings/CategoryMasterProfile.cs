using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class CategoryMasterProfile : Profile
{
    public CategoryMasterProfile()
    {
        CreateMap<CategoryMasterDto, CategoryMaster>()
            .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.CategoryName ?? string.Empty));
    }
}