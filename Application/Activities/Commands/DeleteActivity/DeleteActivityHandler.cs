using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.DeleteActivity;

public class DeleteActivityHandler(AppDbContext context)
    : IRequestHandler<DeleteActivityCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([request.Id], cancellationToken);

        if (activity is null) return Result<Unit>.Failure("Activity not found.", 404);

        context.Activities.Remove(activity);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete an activity.", 400);
    }
}
