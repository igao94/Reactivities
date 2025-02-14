using Application.Activities.Commands.CreateActivity;
using Application.Activities.Commands.DeleteActivity;
using Application.Activities.Commands.EditActivity;
using Application.Activities.Queries.GetActivityById;
using Application.Activities.Queries.GetAllActivities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct)
    {
        return await Mediator.Send(new GetAllActivitiesQuery(), ct);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity?>> GetActivityById(string id, CancellationToken ct)
    {
        return await Mediator.Send(new GetActivityByIdQuery { Id = id }, ct);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateActivity(Activity activity)
    {
        return await Mediator.Send(new CreateActivityCommand { Activity = activity });
    }

    [HttpPut]
    public async Task<ActionResult> EditActivity(Activity activity, CancellationToken ct)
    {
        await Mediator.Send(new EditActivityCommand { Activity = activity }, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteActivityCommand { Id = id }, ct);

        return NoContent();
    }
}
