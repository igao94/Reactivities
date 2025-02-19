using Application.Core;
using MediatR;

namespace Application.Activities.Commands.DeleteActivity;

public class DeleteActivityCommand : IRequest<Result<Unit>>
{
    public required string Id { get; set; }
}
