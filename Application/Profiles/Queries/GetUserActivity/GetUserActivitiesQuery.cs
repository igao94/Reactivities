using Application.Core;
using Application.Profiles.DTOs;
using MediatR;

namespace Application.Profiles.Queries.GetUserActivity;

public class GetUserActivitiesQuery : IRequest<Result<List<UserActivityDto>>>
{
    public required string UserId { get; set; }
    public required string Filter { get; set; }
}
