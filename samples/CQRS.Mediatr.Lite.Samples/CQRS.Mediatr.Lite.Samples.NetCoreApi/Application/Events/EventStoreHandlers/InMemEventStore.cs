using System.Collections.Generic;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Events.EventStoreHandlers
{
    public class InMemEventStore : IEventStore
    {
        private static IList<Event> _events = new List<Event>();
        public void AddEvent(Event @event)
        {
            _events.Add(@event);
        }

        public IEnumerable<Event> Get()
        {
            return _events;
        }
    }
}
