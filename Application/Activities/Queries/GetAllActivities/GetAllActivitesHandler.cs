using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries.GetAllActivities;

public class GetAllActivitesHandler(AppDbContext context) 
    : IRequestHandler<GetAllActivitiesQuery, List<Activity>>
{
    public async Task<List<Activity>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await context.Activities.ToListAsync(cancellationToken);
    }
}
