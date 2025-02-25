using Application.Core;
using MediatR;

namespace Application.Profiles.Commands.FollowToggle;

public class FollowToggleCommand : IRequest<Result<Unit>>
{
    public required string TargetUserId { get; set; }
}
