using Application.Activities.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateActivityDto, Activity>();

        CreateMap<EditActivityDto, Activity>();
    }
}
