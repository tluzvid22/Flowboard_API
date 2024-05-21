using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Token.Delete
{

    public record DeleteTokenByTokenValueRequest(string Token) : IRequest<Result<bool>>;

}