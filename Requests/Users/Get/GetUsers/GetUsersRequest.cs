using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Users.Get
{
    public record GetUsersRequest : IRequest<Result<UserDTO[]>>;

}