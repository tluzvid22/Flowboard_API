using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Users.Get
{

    public record GetUserByTokenRequest(string Token) : IRequest<Result<UserDTO[]>>;

}