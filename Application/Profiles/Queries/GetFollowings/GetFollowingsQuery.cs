using Application.Core;
using Application.Profiles.DTOs;
using MediatR;

namespace Application.Profiles.Queries.GetFollowings;

public class GetFollowingsQuery : IRequest<Result<List<UserProfile>>>
{
    public string Predicate { get; set; } = "followers";
    public required string UserId { get; set; }
}
