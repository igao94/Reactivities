using Application.Activities.DTOs;
using MediatR;

namespace Application.Activities.Queries.GetAllActivities;

public class GetAllActivitiesQuery : IRequest<List<ActivityDto>>
{

}
