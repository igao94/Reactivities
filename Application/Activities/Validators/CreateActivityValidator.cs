using Application.Activities.Commands.CreateActivity;
using Application.Activities.DTOs;

namespace Application.Activities.Validators;

public class CreateActivityValidator : BaseActivityValidator<CreateActivityCommand, CreateActivityDto>
{
    public CreateActivityValidator() : base(x => x.ActivityDto)
    {

    }
}
