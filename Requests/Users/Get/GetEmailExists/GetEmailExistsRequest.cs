using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Users.Get
{

    public record GetEmailExistsRequest(string Email) : IRequest<Result<bool>>;

}