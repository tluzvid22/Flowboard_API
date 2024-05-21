using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Users.Get
{

    public record GetUsernameExistsRequest(string Username) : IRequest<Result<bool>>;

}