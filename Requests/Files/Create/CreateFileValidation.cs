using Data.Setup;
using FluentValidation;
using System.Net.NetworkInformation;

namespace API.Requests.Files.Create
{
    public class CreateFileValidation : AbstractValidator<CreateFileRequest>
    {

        public CreateFileValidation(FlowboardContext db) {

            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet("DataFormatValidation", () =>
            {
                RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode("/errors/user-name-invalid")
                .WithMessage($"'{{PropertyName}}' must not be empty or default.")
                .Must(name => !Path.HasExtension(name))
                .WithMessage($"'{{PropertyName}}' must not contain a file extension.");

                RuleFor(x => x.FileType)
                    .NotEmpty()
                    .WithErrorCode("/errors/file-type-invalid")
                    .WithMessage($"'{{PropertyName}}' must not be empty.")
                    .Must(fileType => IsValidFileType(fileType))
                    .WithMessage($"Invalid '{{PropertyName}}'. Allowed file types: .docx, .doc, .pdf, .csv, .xml, .jpg, .jpeg, .png, .pptx, .ppt, .pdf.");
            
            });
        
        }

        private bool IsValidFileType(string fileType)
        {
            string[] allowedExtensions = { ".docx", ".doc", ".pdf", ".csv", ".xml", ".jpg", ".jpeg", ".png", ".pptx", ".ppt", ".pdf" };

            return allowedExtensions.Contains(fileType);
        }
    }
}
