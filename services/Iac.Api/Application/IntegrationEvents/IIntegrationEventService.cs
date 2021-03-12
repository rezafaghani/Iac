using System;
using System.Threading.Tasks;
using EventBus.Events;

namespace Iac.Api.Application.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}