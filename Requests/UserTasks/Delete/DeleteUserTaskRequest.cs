using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.UserTasks.Delete
{
    public record DeleteUserTaskRequest(int UserId, int TaskId) : IRequest<Result<bool>>;
}