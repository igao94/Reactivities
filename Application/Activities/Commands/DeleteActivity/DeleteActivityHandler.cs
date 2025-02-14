using MediatR;
using Persistence;

namespace Application.Activities.Commands.DeleteActivity;

public class DeleteActivityHandler(AppDbContext context) : IRequestHandler<DeleteActivityCommand>
{
    public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([request.Id], cancellationToken)
            ?? throw new Exception("Activity not found.");

        context.Activities.Remove(activity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
