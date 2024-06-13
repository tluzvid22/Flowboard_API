using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.List.Delete
{
    public class DeleteListByIdValidation : AbstractValidator<DeleteListByIdRequest>
    {

		public DeleteListByIdValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Lists.AsNoTracking().AnyAsync(list => list.Id == request.ListId);
                })
                .WithErrorCode("/errors/list-invalid")
                .WithMessage("List must exist.");
            });

        }
    }
}
