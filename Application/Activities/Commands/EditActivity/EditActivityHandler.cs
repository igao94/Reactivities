using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.EditActivity;

public class EditActivityHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<EditActivityCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(EditActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([request.ActivityDto.Id], cancellationToken);

        if (activity is null) return Result<Unit>.Failure("Activity not found.", 404);

        mapper.Map(request.ActivityDto, activity);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update an activity.", 400);
    }
}
