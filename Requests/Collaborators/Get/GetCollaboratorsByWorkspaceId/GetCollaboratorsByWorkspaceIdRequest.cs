using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Collaborators.Get
{

    public record GetCollaboratorsByWorkspaceIdRequest(int WorkspaceId, int UserId, string UserToken) : IRequest<Result<CollaboratorDTO[]>>;

}