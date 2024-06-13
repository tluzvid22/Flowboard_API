using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Task.Update
{
    public class UpdateTaskValidation : AbstractValidator<UpdateTaskRequest>
    {

		public UpdateTaskValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x.Name).NotEmpty()
                    .WithErrorCode("/errors/user-name-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Tasks.AsNoTracking().AnyAsync(task => task.Id == request.Id);
                })
                .WithErrorCode("/errors/task-invalid")
                .WithMessage("TaskId must exist.");

                RuleFor(x => x).MustAsync( async (request, cancellation) =>
                {
                    if (request.Order < 0) return false;
                    if (await db.Tasks.Where(task => task.ListId == request.ListId).AsNoTracking().AnyAsync(cancellation))
                    {
                        var order = db.Tasks.Where(task => task.ListId == task.ListId).AsNoTracking().Max(task => task.Order);
                        if (request.Order > order) return false;
                    }
                    return true;
                })
                .WithErrorCode("/errors/task-invalid")
                .WithMessage("Order must be lineal from the previous task and >= than 0.");


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
