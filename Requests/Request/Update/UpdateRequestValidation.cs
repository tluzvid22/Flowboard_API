using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Request.Update
{
    public class UpdateRequestValidation : AbstractValidator<UpdateRequestRequest>
    {

		public UpdateRequestValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Requests.AsNoTracking().AnyAsync(r => (r.SenderId == request.SenderId && r.ReceiverId == request.ReceiverId)
                    || (r.SenderId == request.ReceiverId && r.ReceiverId == request.SenderId));
                })
                .WithErrorCode("/errors/request-invalid")
                .WithMessage("Request must exist.");
            });            
        }
    }
}
