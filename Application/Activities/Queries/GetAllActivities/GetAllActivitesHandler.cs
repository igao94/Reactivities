using Application.Activities.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries.GetAllActivities;

public class GetAllActivitesHandler(AppDbContext context,
    IMapper mapper) : IRequestHandler<GetAllActivitiesQuery, List<ActivityDto>>
{
    public async Task<List<ActivityDto>> Handle(GetAllActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        return await context.Activities
            .ProjectTo<ActivityDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
