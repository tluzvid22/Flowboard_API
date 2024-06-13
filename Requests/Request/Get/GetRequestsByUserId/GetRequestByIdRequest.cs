using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Request.Get
{

    public record GetRequestByIdRequest(int UserId) : IRequest<Result<RequestDTO[]>>;

}