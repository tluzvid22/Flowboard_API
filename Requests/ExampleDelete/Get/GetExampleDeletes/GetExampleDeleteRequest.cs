using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.ExampleDelete.Get
{
    public record GetExampleDeleteRequest : IRequest<Result<UserDTO[]>>;

}