using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.List.Delete
{

    public record DeleteListByIdRequest(int ListId) : IRequest<Result<bool>>;

}