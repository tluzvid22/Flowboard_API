using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Token.Create
{
    public record CreateTokenRequest(string Value, int UserId, DateTime Expires) : IRequest<Result<TokenDTO>>;

}