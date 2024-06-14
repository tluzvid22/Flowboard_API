using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Workspace.Get
{

    public record GetCollaboratingWorkspaceByUserIdRequest(int UserId, string Token) : IRequest<Result<WorkspaceDTO[]>>;

}