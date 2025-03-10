﻿using Application.Activities.DTOs;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries.GetActivityById;

public class GetActivityByIdHandler(AppDbContext context,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetActivityByIdQuery, Result<ActivityDto>>
{
    public async Task<Result<ActivityDto>> Handle(GetActivityByIdQuery request,
        CancellationToken cancellationToken)
    {
        var activity = await context.Activities
            .ProjectTo<ActivityDto>(mapper.ConfigurationProvider,
                new { currentUserId = userAccessor.GetUserId() })
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (activity is null) return Result<ActivityDto>.Failure("Activity not found.", 404);

        return Result<ActivityDto>.Success(activity);
    }
}
