using Application.Activities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Activities.Queries.GetActivityById;

public class GetActivityByIdQuery : IRequest<Result<ActivityDto>>
{
    public required string Id { get; set; }
}
