using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Collaborators.Update
{
    public class UpdateCollaboratorValidation : AbstractValidator<UpdateCollaboratorRequest>
    {

		public UpdateCollaboratorValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Collaborator.AsNoTracking().AnyAsync(collaborator => collaborator.UserId == request.UserId && collaborator.WorkspaceId == request.WorkspaceId, cancellation);
                })
                .WithErrorCode("/errors/collaborator-invalid")
                .WithMessage("Collaborator must exist.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Collaborator
                    .Include(c => c.User).ThenInclude(u => u.Token)
                    .AsNoTracking()
                    .AnyAsync(collaborator => collaborator.IsAdmin && collaborator.WorkspaceId == request.WorkspaceId && collaborator.UserId == request.AdminId && collaborator.User.Token.Value == request.AdminToken);
                })
                .WithErrorCode("/errors/admin-invalid")
                .WithMessage("Valid auth of admin collaborator is mandatory for update of collaborators");
            });            
        }
    }
}
