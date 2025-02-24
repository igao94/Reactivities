using Application.Activities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Activities.Commands.AddComment;

public class AddComentCommand : IRequest<Result<CommentDto>>
{
    public required string ActivityId { get; set; }
    public required string Body { get; set; }
}
