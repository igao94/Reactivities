using Application.Core;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Activities.Queries.GetActivityById;

public class GetActivityByIdHandler(AppDbContext context)
    : IRequestHandler<GetActivityByIdQuery, Result<Activity>>
{
    public async Task<Result<Activity>> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([request.Id], cancellationToken);

        if (activity is null) return Result<Activity>.Failure("Activity not found.", 404);

        return Result<Activity>.Success(activity);
    }
}
