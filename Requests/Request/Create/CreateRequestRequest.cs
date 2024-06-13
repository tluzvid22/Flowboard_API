using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Request.Create
{

    public record CreateRequestRequest(int SenderId, int ReceiverId) : IRequest<Result<RequestDTO>>;

}