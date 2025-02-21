using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityHandler(AppDbContext context,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<CreateActivityCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        var activity = mapper.Map<Activity>(request.ActivityDto);

        context.Activities.Add(activity);

        var attendee = new ActivityAttendee
        {
            ActivityId = activity.Id,
            UserId = user.Id,
            IsHost = true
        };

        activity.Attendees.Add(attendee);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<string>.Success(activity.Id)
            : Result<string>.Failure("Failed to create an activity.", 400);
    }
}
