using Application.Activities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Activities.Commands.CreateActivity;

public class CreateActivityCommand : IRequest<Result<string>>
{
    public required CreateActivityDto ActivityDto { get; set; }
}
