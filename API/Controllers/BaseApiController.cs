using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator is unavailable.");

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (!result.IsSuccess && result.StatusCode == 404) return NotFound(result.Error);

        if (result.IsSuccess && result.Value is not null) return Ok(result.Value);

        return BadRequest(result.Error);
    }
}
