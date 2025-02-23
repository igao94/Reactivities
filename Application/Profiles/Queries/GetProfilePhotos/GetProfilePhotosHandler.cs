using Application.Core;
using Application.Profiles.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries.GetProfilePhotos;

public class GetProfilePhotosHandler(AppDbContext context,
    IMapper mapper) : IRequestHandler<GetProfilePhotosQuery, Result<List<PhotoDto>>>
{
    public async Task<Result<List<PhotoDto>>> Handle(GetProfilePhotosQuery request,
        CancellationToken cancellationToken)
    {
        var photos = await context.Users
            .Where(u => u.Id == request.UserId)
            .SelectMany(u => u.Photos)
            .ToListAsync(cancellationToken);

        return Result<List<PhotoDto>>.Success(mapper.Map<List<PhotoDto>>(photos));
    }
}
