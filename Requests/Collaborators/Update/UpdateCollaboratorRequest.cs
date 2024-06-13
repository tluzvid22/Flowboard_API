using API.DTOs;
using Data.Enums;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Requests.Collaborators.Update
{
    public record UpdateCollaboratorRequest([FromHeader] int AdminId, [FromHeader] string AdminToken, int UserId, int WorkspaceId, bool CanRead, bool CanDelete, bool CanModify) : IRequest<Result<CollaboratorDTO>>;
}