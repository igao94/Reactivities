using Application.Core;
using Application.Interfaces;
using Application.Profiles.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Profiles.Commands.AddPhoto;

public class AddPhotoHandler(AppDbContext context,
    IUserAccessor userAccessor,
    IPhotoService photoService,
    IMapper mapper) : IRequestHandler<AddPhotoCommand, Result<PhotoDto>>
{
    public async Task<Result<PhotoDto>> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        var uploadResult = await photoService.UploadPhotoAsync(request.File);

        if (uploadResult is null) return Result<PhotoDto>.Failure("Failed to upload photo.", 400);

        var photo = new Photo
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url,
            UserId = user.Id
        };

        user.ImageUrl ??= photo.Url;

        context.Photos.Add(photo);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<PhotoDto>.Success(mapper.Map<PhotoDto>(photo))
            : Result<PhotoDto>.Failure("Problem saving photo to database.", 400);
    }
}
