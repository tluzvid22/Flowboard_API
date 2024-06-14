using API.DTOs;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Requests.Collaborators.Create
{
    public record CreateCollaboratorRequest([FromHeader] int AdminId, [FromHeader] string AdminToken, int UserId, int WorkspaceId, bool CanRead, bool CanDelete, bool CanModify) : IRequest<Result<CollaboratorDTO>>;

    public class CreateCollaboratorRequestBody
    {
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
        public bool CanRead { get; set; }
        public bool CanDelete { get; set; }
        public bool CanModify { get; set; }
    }
}