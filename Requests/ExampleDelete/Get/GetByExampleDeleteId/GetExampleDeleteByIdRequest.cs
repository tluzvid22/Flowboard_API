using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.ExampleDelete.Get
{

    public record GetExampleDeleteByIdRequest(int Id) : IRequest<Result<UserDTO>>;

}