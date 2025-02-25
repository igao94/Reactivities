using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Profiles.Commands.FollowToggle;

public class FollowToggleHandler(AppDbContext context,
    IUserAccessor userAccessor) : IRequestHandler<FollowToggleCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(FollowToggleCommand request, CancellationToken cancellationToken)
    {
        var observer = await userAccessor.GetUserAsync();

        var target = await context.Users.FindAsync([request.TargetUserId], cancellationToken);

        if (target is null) return Result<Unit>.Failure("User not found.", 400);

        var following = await context.UserFollowings
            .FindAsync([observer.Id, target.Id], cancellationToken);

        if (following is null)
        {
            context.UserFollowings.Add(new UserFollowing
            {
                ObserverId = observer.Id,
                TargetId = target.Id
            });
        }
        else
        {
            context.UserFollowings.Remove(following);
        }

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to follow user.", 400);
    }
}
