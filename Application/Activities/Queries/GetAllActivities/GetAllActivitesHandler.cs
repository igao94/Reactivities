using Application.Activities.DTOs;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries.GetAllActivities;

public class GetAllActivitesHandler(AppDbContext context,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllActivitiesQuery, Result<PagedList<ActivityDto, DateTime?>>>
{
    public async Task<Result<PagedList<ActivityDto, DateTime?>>> Handle(GetAllActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var query = context.Activities
            .OrderBy(a => a.Date)
            .Where(a => a.Date >= (request.ActivityParams.Cursor ?? request.ActivityParams.StartDate))
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.ActivityParams.Filter))
        {
            query = request.ActivityParams.Filter switch
            {
                "isGoing" => query.Where(a => a.Attendees.Any(aa => aa.UserId == userId)),
                "isHost" => query.Where(a => a.Attendees.Any(aa => aa.IsHost && aa.UserId == userId)),
                _ => query
            };
        }

        var projectedActivities = query
            .ProjectTo<ActivityDto>(mapper.ConfigurationProvider, new { currentUser = userId });

        var activities = await projectedActivities
            .Take(request.ActivityParams.PageSize + 1)
            .ToListAsync(cancellationToken);

        DateTime? nextCursor = null;

        if (activities.Count > request.ActivityParams.PageSize)
        {
            nextCursor = activities.Last().Date;
            activities.RemoveAt(activities.Count - 1);
        }

        return Result<PagedList<ActivityDto, DateTime?>>.Success(new PagedList<ActivityDto, DateTime?>
        {
            Items = activities,
            NextCursor = nextCursor
        });
    }
}
