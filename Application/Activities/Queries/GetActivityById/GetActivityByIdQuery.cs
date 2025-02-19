using Application.Core;
using Domain.Entities;
using MediatR;

namespace Application.Activities.Queries.GetActivityById;

public class GetActivityByIdQuery : IRequest<Result<Activity>>
{
    public required string Id { get; set; }
}
