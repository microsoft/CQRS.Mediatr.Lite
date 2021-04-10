using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Events.EventStoreHandlers
{
    public class OrderCreatedEventHandler : EventHandler<OrderCreated>
    {
        private readonly IEventStore _eventStore;

        public OrderCreatedEventHandler(IEventStore eventStore)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        protected override Task<VoidResult> ProcessRequest(OrderCreated request)
        {
            _eventStore.AddEvent(request);
            return Task.FromResult(new VoidResult());
        }
    }
}
