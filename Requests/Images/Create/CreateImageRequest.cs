using API.DTOs;
using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Requests.Images.Create
{
    public record CreateImageRequest(IFormFile File) : IRequest<Result<ImageDTO>>;

}