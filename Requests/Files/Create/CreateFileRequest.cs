using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Files.Create
{
    public record CreateFileRequest(string Name, string FileType, IFormFile File) : IRequest<Result<FileDTO>>;
}