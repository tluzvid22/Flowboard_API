using Data.Setup;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Users.Create
{
    public class CreateUserValidation : AbstractValidator<CreateUserRequest>
    {

		public CreateUserValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x.Name).NotEmpty()
                    .WithErrorCode("/errors/user-name-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x.LastName).NotEmpty()
                    .WithErrorCode("/errors/user-lastname-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x.Address).NotEmpty()
                    .WithErrorCode("/errors/user-address-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x.Password).NotEmpty()
                    .WithErrorCode("/errors/user-password-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x.Email).NotEmpty()
                    .WithErrorCode("/errors/user-email-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.")
                    .EmailAddress()
                    .WithErrorCode("/errors/user-email-invalid")
                    .WithMessage($"'{{PropertyName}}' is not a valid email address.");

                RuleFor(x => x.Username).NotEmpty()
                    .WithErrorCode("/errors/user-username-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x.Phone).NotEmpty()
                    .WithErrorCode("/errors/user-phone-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x.Country).NotEmpty()
                    .WithErrorCode("/errors/user-country-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x.State).NotEmpty()
                    .WithErrorCode("/errors/user-state-invalid")
                    .WithMessage($"'{{PropertyName}}' must not be empty or default.");

                RuleFor(x => x.City).NotEmpty()
                    .WithErrorCode("/errors/user-city-invalid")
                    .WithMessage($"'{{PropertyName}}' must not be empty or default.");

                RuleFor(x => x.ImageId).NotEmpty()
                    .WithErrorCode("/errors/image-invalid")
                    .WithMessage($"'{{PropertyName}}' must not be empty or default.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Image.AsNoTracking().AnyAsync(image => image.Id == request.ImageId);                    
                })
                .WithErrorCode("/errors/image-invalid")
                .WithMessage("ImageId must exist."); ;
            });

        }
    }
}
