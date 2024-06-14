using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Collaborators.Update
{
    public record UpdateCollaboratorRequest(int AdminId, string AdminToken, int UserId, int WorkspaceId, bool CanRead, bool CanDelete, bool CanModify) : IRequest<Result<CollaboratorDTO>>;

    public class UpdateCollaboratorRequestBody
    {
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
        public bool CanRead { get; set; }
        public bool CanDelete { get; set; }
        public bool CanModify { get; set; }
    }
}