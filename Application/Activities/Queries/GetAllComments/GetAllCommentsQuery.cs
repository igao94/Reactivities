using Application.Activities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Activities.Queries.GetAllComments;

public class GetAllCommentsQuery : IRequest<Result<List<CommentDto>>>
{
    public required string ActivityId { get; set; }
}
