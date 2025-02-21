using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence;

namespace Application.Activities.Commands.UpdateAttendance;

public class UpdateAttendanceHandler(AppDbContext context,
    IUserAccessor userAccessor) : IRequestHandler<UpdateAttendanceCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        var activity = await context.Activities
            .Include(a => a.Attendees)
            .ThenInclude(aa => aa.User)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (activity is null) return Result<Unit>.Failure("Activity not found.", 404);

        var isHost = activity.Attendees.Any(aa => aa.IsHost && aa.UserId == user.Id);

        var attendance = activity.Attendees
            .FirstOrDefault(u => u.UserId == user.Id && u.ActivityId == activity.Id);

        if (attendance is not null)
        {
            if (isHost) activity.IsCancelled = !activity.IsCancelled;
            else activity.Attendees.Remove(attendance);
        }
        else
        {
            activity.Attendees.Add(new ActivityAttendee
            {
                ActivityId = activity.Id,
                UserId = user.Id,
                IsHost = false
            });
        }

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update attendance.", 400);
    }
}
