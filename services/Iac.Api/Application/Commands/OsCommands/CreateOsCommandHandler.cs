using System.Threading;
using System.Threading.Tasks;
using Iac.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Application.Commands.OsCommands
{
    public class CreateOsCommandHandler:IRequestHandler<CreateOsCommand,bool>
    {
        public Task<bool> Handle(CreateOsCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
    public class CreateOsIdentifiedCommandHandler : IdentifiedCommandHandler<CreateOsCommand, bool>
    {
        public CreateOsIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreateOsCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
}