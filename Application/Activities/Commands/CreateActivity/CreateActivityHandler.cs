using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityHandler(AppDbContext context,
    IMapper mapper) : IRequestHandler<CreateActivityCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = mapper.Map<Activity>(request.ActivityDto);

        context.Activities.Add(activity);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<string>.Success(activity.Id)
            : Result<string>.Failure("Failed to create an activity.", 400);
    }
}
