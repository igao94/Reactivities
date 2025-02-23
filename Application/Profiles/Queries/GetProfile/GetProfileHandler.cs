using Application.Core;
using Application.Profiles.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries.GetProfile;

public class GetProfileHandler(AppDbContext context,
    IMapper mapper) : IRequestHandler<GetProfileQuery, Result<UserProfile>>
{
    public async Task<Result<UserProfile>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var profile = await context.Users
            .ProjectTo<UserProfile>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        return profile is null
            ? Result<UserProfile>.Failure("Profile not found.", 404)
            : Result<UserProfile>.Success(profile);
    }
}
