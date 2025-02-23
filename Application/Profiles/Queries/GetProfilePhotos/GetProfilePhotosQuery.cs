using Application.Core;
using Application.Profiles.DTOs;
using MediatR;

namespace Application.Profiles.Queries.GetProfilePhotos;

public class GetProfilePhotosQuery : IRequest<Result<List<PhotoDto>>>
{
    public required string UserId { get; set; }
}
