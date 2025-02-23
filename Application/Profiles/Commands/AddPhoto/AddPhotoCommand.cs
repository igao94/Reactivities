using Application.Core;
using Application.Profiles.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Profiles.Commands.AddPhoto;

public class AddPhotoCommand : IRequest<Result<PhotoDto>>
{
    public required IFormFile File { get; set; }
}
