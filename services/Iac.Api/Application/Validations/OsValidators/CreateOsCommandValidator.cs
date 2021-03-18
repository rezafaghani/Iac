using FluentValidation;
using Iac.Api.Application.Commands.OsCommands.Create;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Application.Validations.OsValidators
{
    public class CreateOsCommandValidator : AbstractValidator<CreateOsCommand>
    {
        public CreateOsCommandValidator(ILogger<CreateOsCommandValidator> logger)
        {
            RuleFor(os => os.Title).NotEmpty().WithMessage("No os title found");
            RuleFor(os => os.ImageName).NotEmpty().WithMessage("No os image found");
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}