using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Request.Delete
{

    public record DeleteRequestByIdRequest(int SenderId, int ReceiverId) : IRequest<Result<bool>>;

}