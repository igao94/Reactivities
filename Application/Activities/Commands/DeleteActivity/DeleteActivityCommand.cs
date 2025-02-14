using MediatR;

namespace Application.Activities.Commands.DeleteActivity;

public class DeleteActivityCommand : IRequest
{
    public required string Id { get; set; }
}
