using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.UserTasks.Get
{

    public record GetUserTasksRequest(int TaskId) : IRequest<Result<UserTaskDTO[]>>;

}