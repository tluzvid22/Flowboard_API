using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Users.Get
{

    public record GetUserByEmailAndPasswordRequest(string email, string password) : IRequest<Result<UserDTO>>;

}