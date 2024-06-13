using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Users.Get
{

    public record GetUserByUsernameRequest(string Username) : IRequest<Result<PublicUserDTO[]>>;

}