using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Task.Delete
{

    public record DeleteTaskByIdRequest(int Id) : IRequest<Result<bool>>;

}