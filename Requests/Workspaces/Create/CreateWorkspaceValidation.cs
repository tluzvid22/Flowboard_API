using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Workspace.Create
{
    public class CreateWorkspaceValidation : AbstractValidator<CreateWorkspaceRequest>
    {

		public CreateWorkspaceValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x.Name).NotEmpty()
                    .WithErrorCode("/errors/user-name-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Users.AsNoTracking().AnyAsync(user => user.Id == request.UserId);                    
                })
                .WithErrorCode("/errors/workspace-invalid")
                .WithMessage("UserId must exist.");
            });

        }
    }
}
