using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries.GetAllComments;

public class GetAllCommentsHandler(AppDbContext context,
    IMapper mapper) : IRequestHandler<GetAllCommentsQuery, Result<List<CommentDto>>>
{
    public async Task<Result<List<CommentDto>>> Handle(GetAllCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var comments = await context.Comments
           .Where(c => c.ActivityId == request.ActivityId)
           .OrderByDescending(c => c.CreatedAt)
           .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);

        return Result<List<CommentDto>>.Success(comments);
    }
}
