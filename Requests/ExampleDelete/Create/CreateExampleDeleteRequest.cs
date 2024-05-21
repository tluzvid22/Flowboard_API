using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.ExampleDelete.Create
{
    public record CreateExampleDeleteRequest : IRequest<Result<UserDTO>>
    {

    }

}