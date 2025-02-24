using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.Profiles.Commands.EditProfile;

public class EditProfileHandler(AppDbContext context,
    IUserAccessor userAccessor) : IRequestHandler<EditProfileCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        user.DisplayName = request.DisplayName;
        user.Bio = request.Bio;

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update profile.", 400);
    }
}
