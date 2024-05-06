using Data.Setup;
using FluentValidation;

namespace API.Requests.Images.Create
{
    public class CreateImageValidation : AbstractValidator<CreateImageRequest>
    {

		public CreateImageValidation() { 
			
			RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                //ToDo: Image validation  
            });

        }
    }
}
