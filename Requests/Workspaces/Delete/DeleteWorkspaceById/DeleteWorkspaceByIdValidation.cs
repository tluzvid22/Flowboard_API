using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Workspace.Delete
{
    public class DeleteWorkspaceByIdValidation : AbstractValidator<DeleteWorkspaceByIdRequest>
    {

		public DeleteWorkspaceByIdValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Workspaces.AsNoTracking().AnyAsync(workspace => workspace.Id == request.WorkspaceId);
                })
                .WithErrorCode("/errors/workspace-invalid")
                .WithMessage("Workspace must exist.");
            });

        }
    }
}
