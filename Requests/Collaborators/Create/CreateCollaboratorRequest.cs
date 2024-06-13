using API.DTOs;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Requests.Collaborators.Create
{
    public record CreateCollaboratorRequest([FromHeader] int AdminId, [FromHeader] string AdminToken, int UserId, int WorkspaceId, bool CanRead, bool CanDelete, bool CanModify) : IRequest<Result<CollaboratorDTO>>;
}