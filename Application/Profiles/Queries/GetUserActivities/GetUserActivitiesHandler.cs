using Application.Core;
using Application.Profiles.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries.GetUserActivities;

public class GetUserActivitiesHandler(AppDbContext context,
    IMapper mapper) : IRequestHandler<GetUserActivitiesQuery, Result<List<UserActivityDto>>>
{
    public async Task<Result<List<UserActivityDto>>> Handle(GetUserActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.ActivityAttendees
            .Where(aa => aa.User.Id == request.UserId)
            .OrderBy(aa => aa.Activity.Date)
            .Select(aa => aa.Activity)
            .AsQueryable();

        var today = DateTime.UtcNow;

        query = request.Filter switch
        {
            "past" => query.Where(a => a.Date <= today && a.Attendees.Any(aa => aa.UserId == request.UserId)),
            "hosting" => query.Where(a => a.Attendees.Any(aa => aa.IsHost && aa.UserId == request.UserId)),
            _ => query.Where(a => a.Date >= today && a.Attendees.Any(aa => aa.UserId == request.UserId))
        };

        var projectedActivities = query.ProjectTo<UserActivityDto>(mapper.ConfigurationProvider);

        var activities = await projectedActivities.ToListAsync(cancellationToken);

        return Result<List<UserActivityDto>>.Success(activities);
    }
}
