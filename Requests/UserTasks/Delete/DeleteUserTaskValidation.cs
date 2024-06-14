using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.UserTasks.Delete
{
    public class DeleteUserTaskByWorkspaceAndUserIdsValidation : AbstractValidator<DeleteUserTaskRequest>
    {

		public DeleteUserTaskByWorkspaceAndUserIdsValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.UserTask.AsNoTracking().AnyAsync(userTask => userTask.UserId == request.UserId && userTask.TaskId == request.TaskId, cancellation);
                })
                .WithErrorCode("/errors/userTask-invalid")
                .WithMessage("UserTask must exist.");
            });
        }
    }
}
