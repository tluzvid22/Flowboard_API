using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Collaborators.Delete
{
    public record DeleteCollaboratorByWorkspaceAndUserIdsRequest(int AdminId, string AdminToken, int UserId, int WorkspaceId) : IRequest<Result<bool>>;
}