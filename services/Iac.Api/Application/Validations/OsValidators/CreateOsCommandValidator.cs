using FluentValidation;
using Iac.Api.Application.Commands.OsCommands;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Application.Validations.OsValidators
{
    public class CreateOsCommandValidator : AbstractValidator<CreateOsCommand>
    {
        public CreateOsCommandValidator(ILogger<CreateOsCommandValidator> logger)
        {
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}