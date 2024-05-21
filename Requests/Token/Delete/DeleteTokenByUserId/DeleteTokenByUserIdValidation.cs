using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Token.Delete
{
    public class DeleteTokenByUserIdValidation : AbstractValidator<DeleteTokenByUserIdRequest>
    {

		public DeleteTokenByUserIdValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Tokens.AsNoTracking().AnyAsync(token => token.UserId == request.UserId);
                })
                .WithErrorCode("/errors/token-invalid")
                .WithMessage("Token must exist.");
            });

        }
    }
}
