using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Files.Create
{
    public class UpdateTaskIdOnFileValidation : AbstractValidator<UpdateTaskIdOnFileRequest>
    {

		public UpdateTaskIdOnFileValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Files.AsNoTracking().AnyAsync(task => task.Id == request.Id);
                })
                .WithErrorCode("/errors/file-invalid")
                .WithMessage("FileId must exist.");
            });

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Tasks.AsNoTracking().AnyAsync(task => task.Id == request.TaskId);
                })
                .WithErrorCode("/errors/file-invalid")
                .WithMessage("TaskId must exist.");
            });

        }
    }
}
