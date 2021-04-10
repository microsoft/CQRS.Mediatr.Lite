using System.Collections.Generic;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Events.EventStoreHandlers
{
    public interface IEventStore
    {
        void AddEvent(Event @event);
        IEnumerable<Event> Get();
    }
}