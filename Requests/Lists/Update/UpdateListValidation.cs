using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.List.Update
{
    public class UpdateListValidation : AbstractValidator<UpdateListRequest>
    {

		public UpdateListValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x.Name).NotEmpty()
                    .WithErrorCode("/errors/user-name-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Lists.AsNoTracking().AnyAsync(list => list.Id == request.Id);
                })
                .WithErrorCode("/errors/list-invalid")
                .WithMessage("ListId must exist.");

                RuleFor(x => x).MustAsync( async (request, cancellation) =>
                {
                    if (request.Order < 0) return false;
                    if (await db.Lists.Where(list => list.WorkspaceId == request.WorkspaceId).AsNoTracking().AnyAsync(cancellation))
                    {
                        var order = db.Lists.Where(list => list.WorkspaceId == request.WorkspaceId).AsNoTracking().Max(list => list.Order);
                        if (request.Order > order) return false;
                    }   
                    return true;
                })
                .WithErrorCode("/errors/list-invalid")
                .WithMessage("Order must be lineal from the previous list and >= than 0.");


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
