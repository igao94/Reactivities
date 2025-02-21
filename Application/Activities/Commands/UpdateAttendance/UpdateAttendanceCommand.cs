using Application.Core;
using MediatR;

namespace Application.Activities.Commands.UpdateAttendance;

public class UpdateAttendanceCommand : IRequest<Result<Unit>>
{
    public required string Id { get; set; }
}
