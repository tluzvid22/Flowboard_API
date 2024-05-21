using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Workspace.Get
{

    public record GetWorkspaceByIdRequest(int WorkspaceId, int UserId, string Token) : IRequest<Result<WorkspaceDTO>>;

}