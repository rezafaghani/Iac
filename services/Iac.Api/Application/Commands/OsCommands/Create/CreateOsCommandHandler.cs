using System.Threading;
using System.Threading.Tasks;
using Iac.Domain.AggregateModels;
using Iac.Domain.SeedWork;
using Iac.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Application.Commands.OsCommands.Create
{
    public class CreateOsCommandHandler:IRequestHandler<CreateOsCommand,bool>
    {
        private readonly IRepository<OsEntity> _osRepository;

        public CreateOsCommandHandler(IRepository<OsEntity> osRepository)
        {
            _osRepository = osRepository;
        }

        public async Task<bool> Handle(CreateOsCommand request, CancellationToken cancellationToken)
        {
            _osRepository.Add(new OsEntity
            {
                Title = request.Title,
                Version = request.Version,
                ImageName = request.ImageName,
                LocationId = request.LocationId
            });
           return await _osRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
           
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