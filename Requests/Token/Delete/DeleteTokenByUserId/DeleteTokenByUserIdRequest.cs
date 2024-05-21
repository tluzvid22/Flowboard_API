using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Token.Delete
{

    public record DeleteTokenByUserIdRequest(int UserId) : IRequest<Result<bool>>;

}