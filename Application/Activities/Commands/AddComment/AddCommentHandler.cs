using Application.Activities.DTOs;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands.AddComment;

public class AddCommentHandler(AppDbContext context,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<AddComentCommand, Result<CommentDto>>
{
    public async Task<Result<CommentDto>> Handle(AddComentCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetUserAsync();

        var activity = await context.Activities
            .Include(a => a.Comments)
                .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(u => u.Id == request.ActivityId, cancellationToken);

        if (activity is null) return Result<CommentDto>.Failure("Activity not found.", 404);

        var comment = new Comment
        {
            UserId = user.Id,
            ActivityId = activity.Id,
            Body = request.Body
        };

        activity.Comments.Add(comment);

        var result = await context.SaveChangesAsync(cancellationToken) > 0;

        return result
            ? Result<CommentDto>.Success(mapper.Map<CommentDto>(comment))
            : Result<CommentDto>.Failure("Failed to add comment.", 400);
    }
}
