using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;
 
namespace PreSchoolManagement.Application.Features.Mappings;

public class CommitteeMasterProfile : Profile
{
    public CommitteeMasterProfile()
    {
        CreateMap<CommitteeMasterDto, CommitteeMaster>()
            .ForMember(d => d.CommitteeId,opt =>opt.MapFrom(s => s.CommitteeId))
            .ForMember(d => d.CommitteeName,opt => opt.MapFrom(s => s.CommitteeName??string.Empty));
    }
}