using API.DTOs;
using Data.Enums;
using FluentResults;
using MediatR;

namespace API.Requests.Request.Update
{
    public record UpdateRequestRequest(int SenderId, int ReceiverId, Status Status): IRequest<Result<RequestDTO>>;
}