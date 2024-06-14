using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Collaborators.Get
{
    public class GetCollaboratorsByUserIdValidation : AbstractValidator<GetCollaboratorsByUserIdRequest>
    {

		public GetCollaboratorsByUserIdValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Users
                    .AsNoTracking()
                    .AnyAsync(user => user.Id == request.UserId, cancellation);
                })
                .WithErrorCode("/errors/collaborator-invalid")
                .WithMessage("User must exist.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Collaborator
                    .Include(c => c.User).ThenInclude(u => u.Token)
                    .AsNoTracking()
                    .AnyAsync(collaborator => collaborator.UserId == request.UserId && collaborator.User.Token.Value == request.UserToken, cancellation);
                })
                .WithErrorCode("/errors/collaborator-invalid")
                .WithMessage("Token does not exist");
            });
        }
    }
}
