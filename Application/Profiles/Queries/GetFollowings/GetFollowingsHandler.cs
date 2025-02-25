using Application.Core;
using Application.Interfaces;
using Application.Profiles.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries.GetFollowings;

public class GetFollowingsHandler(AppDbContext context,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetFollowingsQuery, Result<List<UserProfile>>>
{
    public async Task<Result<List<UserProfile>>> Handle(GetFollowingsQuery request,
        CancellationToken cancellationToken)
    {
        List<UserProfile> profiles = [];

        switch (request.Predicate)
        {
            case "followers":
                profiles = await context.UserFollowings
                    .Where(uf => uf.TargetId == request.UserId)
                    .Select(uf => uf.Observer)
                    .ProjectTo<UserProfile>(mapper.ConfigurationProvider,
                        new { currentUserId = userAccessor.GetUserId() })
                    .ToListAsync(cancellationToken);
                break;

            case "followings":
                profiles = await context.UserFollowings
                    .Where(uf => uf.ObserverId == request.UserId)
                    .Select(uf => uf.Target)
                    .ProjectTo<UserProfile>(mapper.ConfigurationProvider,
                        new { currentUserId = userAccessor.GetUserId() })
                    .ToListAsync(cancellationToken);
                break;
        }

        return Result<List<UserProfile>>.Success(profiles);
    }
}
