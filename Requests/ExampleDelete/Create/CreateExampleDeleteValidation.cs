using Data.Setup;
using FluentValidation;

namespace API.Requests.ExampleDelete.Create
{
    public class CreateExampleDeleteValidation : AbstractValidator<CreateExampleDeleteRequest>
    {

		public CreateExampleDeleteValidation(FlowboardContext db) { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                /*RuleFor(x => x.Name).NotEmpty()
                    .WithErrorCode("/errors/user-name-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x.LastName).NotEmpty()
                    .WithErrorCode("/errors/user-lastname-invalid")
                    .WithMessage($"'{{PropertyName}}'  must not be empty or default.");

                RuleFor(x => x).MustAsync(async (request, cancellation) =>
                {
                    return await db.Image.AsNoTracking().AnyAsync(image => image.Id == request.ImageId);                    
                })
                .WithErrorCode("/errors/image-invalid")
                .WithMessage("ImageId must exist.");*/
            });

        }
    }
}
