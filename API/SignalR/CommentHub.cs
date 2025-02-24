using Application.Activities.Commands.AddComment;
using Application.Activities.Queries.GetAllComments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR;

public class CommentHub(IMediator mediator) : Hub
{
    public async Task SendComment(AddComentCommand command)
    {
        var comment = await mediator.Send(command);

        await Clients.Group(command.ActivityId).SendAsync("ReceivedComment", comment.Value);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();

        var activityId = httpContext?.Request.Query["activityId"];

        if (string.IsNullOrEmpty(activityId)) throw new HubException("No activity with this id.");

        await Groups.AddToGroupAsync(Context.ConnectionId, activityId!);

        var result = await mediator.Send(new GetAllCommentsQuery { ActivityId = activityId! });

        await Clients.Caller.SendAsync("LoadComments", result.Value);
    }
}
