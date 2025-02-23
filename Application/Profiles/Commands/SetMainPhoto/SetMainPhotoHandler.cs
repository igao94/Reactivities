using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.Profiles.Commands.SetMainPhoto;

public class SetMainPhotoHandler(AppDbContext context,
    IUserAccessor userAccessor) : IRequestHandler<SetMainPhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserWithPhotosAsync();

        var photo = user.Photos.FirstOrDefault(p => p.Id == request.PhotoId);

        if (photo is null) return Result<Unit>.Failure("Photo not found.", 400);

        user.ImageUrl = photo.Url;

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to set main photo.", 400);
    }
}
