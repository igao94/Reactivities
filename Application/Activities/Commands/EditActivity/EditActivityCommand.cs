using Domain.Entities;
using MediatR;

namespace Application.Activities.Commands.EditActivity;

public class EditActivityCommand : IRequest
{
    public required Activity Activity { get; set; }
}
