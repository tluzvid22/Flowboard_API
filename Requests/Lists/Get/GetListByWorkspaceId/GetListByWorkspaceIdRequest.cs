using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.List.Get
{

    public record GetListByWorkspaceIdRequest(int WorkspaceId) : IRequest<Result<ListDTO[]>>;

}