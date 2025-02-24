using Application.Core;
using MediatR;

namespace Application.Profiles.Commands.EditProfile;

public class EditProfileCommand : IRequest<Result<Unit>>
{
    public required string DisplayName { get; set; }
    public string? Bio { get; set; }
}
