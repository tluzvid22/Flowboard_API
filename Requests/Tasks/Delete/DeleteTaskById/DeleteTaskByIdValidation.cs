using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Task.Delete
{
    public class DeleteTaskByIdValidation : AbstractValidator<DeleteTaskByIdRequest>
    {

		public DeleteTaskByIdValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Tasks.AsNoTracking().AnyAsync(Task => Task.Id == request.Id);
                })
                .WithErrorCode("/errors/Task-invalid")
                .WithMessage("Task must exist.");
            });

        }
    }
}
