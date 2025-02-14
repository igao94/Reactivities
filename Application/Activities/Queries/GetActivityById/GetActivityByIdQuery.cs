using Domain.Entities;
using MediatR;

namespace Application.Activities.Queries.GetActivityById;

public class GetActivityByIdQuery : IRequest<Activity>
{
    public required string Id { get; set; }
}
