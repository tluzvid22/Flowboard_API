using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Request.Delete
{
    public class DeleteRequestByIdValidation : AbstractValidator<DeleteRequestByIdRequest>
    {

		public DeleteRequestByIdValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Requests.AsNoTracking().AnyAsync(r => (r.SenderId == request.SenderId && r.ReceiverId == request.ReceiverId)
                    || (r.ReceiverId == request.SenderId && r.SenderId == request.ReceiverId), cancellation);
                })
                .WithErrorCode("/errors/request-invalid")
                .WithMessage("Request must exist.");
            });

        }
    }
}
