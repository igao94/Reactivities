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
        var query = context.Activities
            .OrderBy(a => a.Date)
            .AsQueryable();

        if (request.Cursor.HasValue)
        {
            query = query.Where(a => a.Date >= request.Cursor.Value);
        }

        var activities = await query
            .Take(request.PageSize + 1)
            .ProjectTo<ActivityDto>(mapper.ConfigurationProvider,
                new { currentUser = userAccessor.GetUserId() })
            .ToListAsync(cancellationToken);

        DateTime? nextCursor = null;

        if (activities.Count > request.PageSize)
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
