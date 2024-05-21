using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Task.Get
{

    public record GetTaskByListIdRequest(int ListId) : IRequest<Result<TaskDTO[]>>;

}