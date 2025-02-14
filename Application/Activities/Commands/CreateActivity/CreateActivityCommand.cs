using Domain.Entities;
using MediatR;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityCommand : IRequest<string>
{
    public required Activity Activity { get; set; }
}
