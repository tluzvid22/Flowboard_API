using API.DTOs;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Requests.UserTasks.Create
{
    public record CreateUserTaskRequest(int UserId, int TaskId) : IRequest<Result<UserTaskDTO>>;
}