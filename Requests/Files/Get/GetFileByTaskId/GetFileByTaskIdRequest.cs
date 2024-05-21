using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.File.Get
{

    public record GetFileByTaskIdRequest(int TaskId) : IRequest<Result<FileDTO[]>>;

}