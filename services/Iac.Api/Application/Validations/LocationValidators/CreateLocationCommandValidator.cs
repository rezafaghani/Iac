using FluentValidation;
using Iac.Api.Application.Commands.LocationCommands.Create;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Application.Validations.LocationValidators
{
    public class CreateLocationCommandValidator :AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator(ILogger<CreateLocationCommand> logger)
        {
            RuleFor(loc => loc.Title).NotEmpty().WithMessage("No location title found");
            
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}