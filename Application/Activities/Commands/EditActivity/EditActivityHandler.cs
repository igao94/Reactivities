using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.EditActivity;

public class EditActivityHandler(AppDbContext context, IMapper mapper) : IRequestHandler<EditActivityCommand>
{
    public async Task Handle(EditActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken) 
            ?? throw new Exception("Activity not found.");

        mapper.Map(request.Activity, activity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
