using Domain.Entities;
using MediatR;

namespace Application.Activities.Queries.GetAllActivities;

public class GetAllActivitiesQuery : IRequest<List<Activity>>
{

}
