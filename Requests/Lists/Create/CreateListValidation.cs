using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.List.Create
{
    public class CreateListValidation : AbstractValidator<CreateListRequest>
    {

		public CreateListValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x.Name).NotEmpty()
                    .WithErrorCode("/errors/user-name-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Workspaces.AsNoTracking().AnyAsync(workspace => workspace.Id == request.WorkspaceId);                    
                })
                .WithErrorCode("/errors/list-invalid")
                .WithMessage("WorkspaceId must exist.");
            });

        }
    }
}
