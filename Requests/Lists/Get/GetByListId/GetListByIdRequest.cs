using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.List.Get
{

    public record GetListByIdRequest(int ListId) : IRequest<Result<ListDTO>>;

}