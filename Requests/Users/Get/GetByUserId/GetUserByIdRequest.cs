using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Users.Get
{

    public record GetUserByIdRequest(int Id) : IRequest<Result<UserDTO>>;

}