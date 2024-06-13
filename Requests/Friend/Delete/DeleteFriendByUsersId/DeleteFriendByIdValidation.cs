using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Friend.Delete
{
    public class DeleteFriendByIdValidation : AbstractValidator<DeleteFriendByIdRequest>
    {

		public DeleteFriendByIdValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Friends.AsNoTracking().AnyAsync(friend => (friend.User1Id == request.User1Id && friend.User2Id == request.User2Id)
                    || (friend.User2Id == request.User1Id && friend.User1Id == request.User2Id));
                })
                .WithErrorCode("/errors/friend-invalid")
                .WithMessage("Friend must exist.");
            });

        }
    }
}
