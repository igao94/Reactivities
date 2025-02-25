using Application.Activities.Commands.CreateActivity;
using Application.Activities.Commands.DeleteActivity;
using Application.Activities.Commands.EditActivity;
using Application.Activities.Commands.UpdateAttendance;
using Application.Activities.DTOs;
using Application.Activities.Queries.GetActivityById;
using Application.Activities.Queries.GetAllActivities;
using Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedList<ActivityDto, DateTime?>>> GetActivities(DateTime? cursor)
    {
        return HandleResult(await Mediator.Send(new GetAllActivitiesQuery { Cursor = cursor }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetActivityById(string id)
    {
        return HandleResult(await Mediator.Send(new GetActivityByIdQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateActivity(CreateActivityDto activityDto)
    {
        return HandleResult(await Mediator.Send(new CreateActivityCommand { ActivityDto = activityDto }));
    }

    [Authorize(Policy = "IsActivityHost")]
    [HttpPut("{id}")]
    public async Task<ActionResult> EditActivity(string id, EditActivityDto activityDto)
    {
        activityDto.Id = id;

        return HandleResult(await Mediator.Send(new EditActivityCommand { ActivityDto = activityDto }));
    }

    [Authorize(Policy = "IsActivityHost")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteActivityCommand { Id = id }));
    }

    [HttpPost("{id}/attend")]
    public async Task<ActionResult> Attend(string id)
    {
        return HandleResult(await Mediator.Send(new UpdateAttendanceCommand { Id = id }));
    }
}
