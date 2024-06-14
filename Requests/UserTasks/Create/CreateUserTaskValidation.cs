using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.UserTasks.Create
{
    public class CreateUserTaskValidation : AbstractValidator<CreateUserTaskRequest>
    {

		public CreateUserTaskValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;
                     
            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Tasks
                    .AsNoTracking()
                    .AnyAsync(task => task.Id == request.TaskId, cancellation);
                })
                .WithErrorCode("/errors/userTask-invalid")
                .WithMessage("Task must exist.");


                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Users
                    .AsNoTracking()
                    .AnyAsync(user => user.Id == request.UserId, cancellation);
                })
                .WithErrorCode("/errors/userTask-invalid")
                .WithMessage("User must exist.");
                
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return !(await db.UserTask
                    .AsNoTracking()
                    .AnyAsync(user => user.UserId == request.UserId && user.TaskId == request.TaskId, cancellation));
                })
                .WithErrorCode("/errors/userTask-invalid")
                .WithMessage("UserTask already exists.");

            });
        }
    }
}
