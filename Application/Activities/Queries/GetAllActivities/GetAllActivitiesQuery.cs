using Application.Activities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Activities.Queries.GetAllActivities;

public class GetAllActivitiesQuery : IRequest<Result<PagedList<ActivityDto, DateTime?>>>
{
    public required ActivityParams ActivityParams { get; set; }
}
