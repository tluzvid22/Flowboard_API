using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Workspace.Delete
{

    public record DeleteWorkspaceByIdRequest(int WorkspaceId, int UserId ,string Token) : IRequest<Result<bool>>;

}