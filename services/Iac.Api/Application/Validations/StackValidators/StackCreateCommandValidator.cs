using FluentValidation;
using Iac.Api.Application.Commands.StackCommands.Create;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Application.Validations.StackValidators
{
    public class StackCreateCommandValidator :AbstractValidator<StackCreateCommand>
    {
        public StackCreateCommandValidator(ILogger<StackCreateCommandValidator> logger)
        {
            RuleFor(os => os.StackName).NotEmpty().WithMessage("No stack name found");
            RuleFor(os => os.OsId).NotEmpty().WithMessage("No os id found");
            RuleFor(os => os.Items).NotEmpty().WithMessage("No item found");
            RuleFor(os => os.Network).NotEmpty().WithMessage("No network found");
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}