using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Task.Create
{
    public class CreateTaskValidation : AbstractValidator<CreateTaskRequest>
    {

		public CreateTaskValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x.Name).NotEmpty()
                    .WithErrorCode("/errors/user-name-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Lists.AsNoTracking().AnyAsync(list => list.Id == request.ListId);                    
                })
                .WithErrorCode("/errors/task-invalid")
                .WithMessage("ListId must exist.");
            });

        }
    }
}
