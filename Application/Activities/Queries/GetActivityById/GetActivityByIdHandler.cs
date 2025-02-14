using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Activities.Queries.GetActivityById;

public class GetActivityByIdHandler(AppDbContext context) : IRequestHandler<GetActivityByIdQuery, Activity>
{
    public async Task<Activity> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([request.Id], cancellationToken);

        if (activity is null) throw new Exception("Activity not found.");

        return activity;
    }
}
