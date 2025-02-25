using Application.Activities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Activities.Queries.GetAllActivities;

public class GetAllActivitiesQuery : IRequest<Result<PagedList<ActivityDto, DateTime?>>>
{
    private const int MaxPageSize = 50;

    public DateTime? Cursor { get; set; }
    private int _pageSize = 3;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
