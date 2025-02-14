using MediatR;
using Persistence;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityHandler(AppDbContext context)
    : IRequestHandler<CreateActivityCommand, string>
{
    public async Task<string> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        context.Activities.Add(request.Activity);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? request.Activity.Id
            : throw new Exception("Failed to create an activity.");
    }
}
