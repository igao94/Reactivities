using Application.Core;
using MediatR;

namespace Application.Profiles.Commands.DeletePhoto;

public class DeletePhotoCommand : IRequest<Result<Unit>>
{
    public required string PhotoId { get; set; }
}
