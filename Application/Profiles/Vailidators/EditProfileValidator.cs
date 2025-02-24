﻿using Application.Profiles.Commands.EditProfile;
using FluentValidation;

namespace Application.Profiles.Vailidators;

public class EditProfileValidator : AbstractValidator<EditProfileCommand>
{
    public EditProfileValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Display name is required.");
    }
}
