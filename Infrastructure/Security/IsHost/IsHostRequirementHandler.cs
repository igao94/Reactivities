﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Claims;

namespace Infrastructure.Security.IsHost;

public class IsHostRequirementHandler(AppDbContext dbContext,
    IHttpContextAccessor httpContextAccessor) : AuthorizationHandler<IsHostRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        IsHostRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null) return;

        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext?.GetRouteValue("id") is not string activityId) return;

        var attendee = await dbContext.ActivityAttendees
            .AsNoTracking()
            .FirstOrDefaultAsync(aa => aa.UserId == userId && aa.ActivityId == activityId);

        if (attendee is null) return;

        if (attendee.IsHost) context.Succeed(requirement);
    }
}
