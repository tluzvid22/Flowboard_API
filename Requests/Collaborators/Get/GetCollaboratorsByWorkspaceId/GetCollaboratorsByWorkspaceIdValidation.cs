using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Collaborators.Get
{
    public class GetCollaboratorsByWorkspaceIdValidation : AbstractValidator<GetCollaboratorsByWorkspaceIdRequest>
    {

		public GetCollaboratorsByWorkspaceIdValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Workspaces
                    .AsNoTracking()
                    .AnyAsync(workspace => workspace.Id == request.WorkspaceId, cancellation);
                })
                .WithErrorCode("/errors/collaborator-invalid")
                .WithMessage("Workspace must exist.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Collaborator
                    .Include(c => c.User).ThenInclude(u => u.Token)
                    .AsNoTracking()
                    .AnyAsync(collaborator => collaborator.UserId == request.UserId && collaborator.WorkspaceId == request.WorkspaceId && collaborator.User.Token.Value == request.UserToken, cancellation);
                })
                .WithErrorCode("/errors/collaborator-invalid")
                .WithMessage("Valid authenticated collaborator must exist in the workspace to show collaborators.");
            });
        }
    }
}
