using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Request.Create
{
    public class CreateRequestValidation : AbstractValidator<CreateRequestRequest>
    {

		public CreateRequestValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x).Must((request) =>
                {
                    return request.ReceiverId != request.SenderId;
                })
                .WithErrorCode("/errors/request-invalid")
                .WithMessage("IDs must be different.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Users.AsNoTracking().AnyAsync(user => user.Id == request.SenderId, cancellation);
                })
                .WithErrorCode("/errors/request-invalid")
                .WithMessage("User1 must exist.");
                
                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Users.AsNoTracking().AnyAsync(user => user.Id == request.ReceiverId, cancellation);
                })
                .WithErrorCode("/errors/request-invalid")
                .WithMessage("User2 must exist.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return !(await db.Requests.AsNoTracking().AnyAsync(r => (r.SenderId == request.SenderId && r.ReceiverId == request.ReceiverId)
                    || (r.ReceiverId == request.SenderId && r.SenderId == request.ReceiverId), cancellation));
                })
                .WithErrorCode("/errors/request-invalid")
                .WithMessage("Request already exists.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return !(await db.Friends.AsNoTracking().AnyAsync(friend => (friend.User1Id == request.SenderId && friend.User2Id == request.ReceiverId)
                    || (friend.User2Id == request.SenderId && friend.User1Id == request.ReceiverId)));
                })
                .WithErrorCode("/errors/friend-invalid")
                .WithMessage("Friend already exists.");
            });

        }
    }
}
