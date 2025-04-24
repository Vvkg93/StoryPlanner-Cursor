using AutoMapper;
using PlanItPoker.Application.DTOs;
using PlanItPoker.Domain.Entities;

namespace PlanItPoker.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sprint, SprintDto>();
            CreateMap<UserStory, UserStoryDto>();
            CreateMap<Vote, VoteDto>();
        }
    }
} 