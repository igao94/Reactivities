using Application.Profiles.Commands.AddPhoto;
using Application.Profiles.Commands.DeletePhoto;
using Application.Profiles.Commands.EditProfile;
using Application.Profiles.Commands.FollowToggle;
using Application.Profiles.Commands.SetMainPhoto;
using Application.Profiles.DTOs;
using Application.Profiles.Queries.GetFollowings;
using Application.Profiles.Queries.GetProfile;
using Application.Profiles.Queries.GetProfilePhotos;
using Application.Profiles.Queries.GetUserActivity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfilesController : BaseApiController
{
    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto([FromForm] AddPhotoCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpGet("{userId}/photos")]
    public async Task<ActionResult<List<PhotoDto>>> GetPhotosForUser(string userId)
    {
        return HandleResult(await Mediator.Send(new GetProfilePhotosQuery { UserId = userId }));
    }

    [HttpDelete("{photoId}/photos")]
    public async Task<ActionResult> DeletePhoto(string photoId)
    {
        return HandleResult(await Mediator.Send(new DeletePhotoCommand { PhotoId = photoId }));
    }

    [HttpPut("{photoId}/setMain")]
    public async Task<ActionResult> SetMainPhoto(string photoId)
    {
        return HandleResult(await Mediator.Send(new SetMainPhotoCommand { PhotoId = photoId }));
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserProfile>> GetProfile(string userId)
    {
        return HandleResult(await Mediator.Send(new GetProfileQuery { UserId = userId }));
    }

    [HttpPut]
    public async Task<ActionResult> EditProfile(EditProfileCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpPost("{userId}/follow")]
    public async Task<ActionResult> FollowToggle(string userId)
    {
        return HandleResult(await Mediator.Send(new FollowToggleCommand { TargetUserId = userId }));
    }

    [HttpGet("{userId}/follow-list")]
    public async Task<ActionResult<List<UserProfile>>> GetFollowings(string userId, string predicate)
    {
        return HandleResult(await Mediator.Send(new GetFollowingsQuery
        {
            UserId = userId,
            Predicate = predicate
        }));
    }

    [HttpGet("{userId}/activities")]
    public async Task<ActionResult<List<UserActivityDto>>> GetUserActivity(string userId, string filter)
    {
        return HandleResult(await Mediator.Send(new GetUserActivitiesQuery
        {
            UserId = userId,
            Filter = filter
        }));
    }
}
