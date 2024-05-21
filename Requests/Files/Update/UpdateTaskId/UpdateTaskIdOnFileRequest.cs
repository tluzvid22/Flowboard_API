using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Files.Create
{
    public record UpdateTaskIdOnFileRequest(int Id, int TaskId) : IRequest<Result<FileDTO>>;
}