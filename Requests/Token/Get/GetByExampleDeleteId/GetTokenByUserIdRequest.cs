using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Token.Get
{

    public record GetTokenByUserIdRequest(int UserId) : IRequest<Result<TokenDTO[]>>;

}