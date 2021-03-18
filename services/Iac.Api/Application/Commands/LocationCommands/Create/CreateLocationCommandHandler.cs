using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iac.Domain.AggregateModels;
using Iac.Domain.Exceptions;
using Iac.Domain.SeedWork;
using Iac.Infrastructure.Idempotency;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Application.Commands.LocationCommands.Create
{
    public class CreateLocationCommandHandler:IRequestHandler<CreateLocationCommand,bool>
    {
        private readonly IRepository<LocationEntity> _locationRepository;

        public CreateLocationCommandHandler(IRepository<LocationEntity> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<bool> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var existBefore=await _locationRepository.GetAll().AnyAsync(x => x.Title.ToLower().Equals(request.Title.ToLower()), cancellationToken: cancellationToken);
            if (existBefore)
                throw new DomainException("this location exist please insert another one");
            _locationRepository.Add(new LocationEntity
            {
                Title = request.Title
            });
            var result= await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result;
        }
    }
    public class CreateLocationIdentifiedCommandHandler : IdentifiedCommandHandler<CreateLocationCommand, bool>
    {
        public CreateLocationIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreateLocationCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
}