using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.Profiles.Commands.DeletePhoto;

public class DeletePhotoHandler(AppDbContext context,
    IUserAccessor userAccessor,
    IPhotoService photoService) : IRequestHandler<DeletePhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserWithPhotosAsync();

        var photo = user.Photos.FirstOrDefault(p => p.Id == request.PhotoId);

        if (photo is null) return Result<Unit>.Failure("Photo not found.", 400);

        if (photo.Url == user.ImageUrl) return Result<Unit>.Failure("Can't delete main photo.", 400);

        await photoService.DeletePhotoAsync(photo.PublicId);

        user.Photos.Remove(photo);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete photo.", 400);
    }
}
