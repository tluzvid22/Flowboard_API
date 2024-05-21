using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Task.Get
{

    public record GetTaskByIdRequest(int TaskId) : IRequest<Result<TaskDTO>>;

}