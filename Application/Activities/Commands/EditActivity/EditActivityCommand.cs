using Application.Activities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Activities.Commands.EditActivity;

public class EditActivityCommand : IRequest<Result<Unit>>
{
    public required EditActivityDto ActivityDto { get; set; }
}
