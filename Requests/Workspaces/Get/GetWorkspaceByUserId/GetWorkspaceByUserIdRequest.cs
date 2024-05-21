using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Workspace.Get
{

    public record GetWorkspaceByUserIdRequest(int UserId, string Token) : IRequest<Result<WorkspaceDTO[]>>;

}