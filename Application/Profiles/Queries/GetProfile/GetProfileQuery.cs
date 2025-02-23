using Application.Core;
using Application.Profiles.DTOs;
using MediatR;

namespace Application.Profiles.Queries.GetProfile;

public class GetProfileQuery : IRequest<Result<UserProfile>>
{
    public required string UserId { get; set; }
}
