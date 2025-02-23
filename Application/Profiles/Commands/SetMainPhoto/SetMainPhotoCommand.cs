using Application.Core;
using MediatR;

namespace Application.Profiles.Commands.SetMainPhoto;

public class SetMainPhotoCommand : IRequest<Result<Unit>>
{
    public required string PhotoId { get; set; }
}
