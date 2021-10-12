using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Bus for sending events
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly IDictionary<Type, object> _eventHandlerWrappers;
        private readonly IRequestHandlerResolver _requestHandlerResolver;

        /// <summary>
        /// Constructs the bus
        /// </summary>
        /// <param name="requestHandlerResolver">Resolved event handlers</param>
        public EventBus(IRequestHandlerResolver requestHandlerResolver)
        {
            _requestHandlerResolver = requestHandlerResolver;
            _eventHandlerWrappers = new ConcurrentDictionary<Type, object>();
        }

        /// <summary>
        /// Sends an event to the bus
        /// </summary>
        /// <param name="event" cref="Event">Event being published</param>
        /// <returns>Completed Task</returns>
        public Task Send(Event @event)
        {
            var eventType = @event.GetType();
            if (!_eventHandlerWrappers.ContainsKey(eventType))
            {
                var eventHandlerWrapper = Activator.CreateInstance(typeof(EventHandlerWrapper<>)
                    .MakeGenericType(eventType));
                _eventHandlerWrappers.Add(eventType, eventHandlerWrapper);
            }
            var eventHandler = (IEventHandlerWrapper)_eventHandlerWrappers[eventType];
            return eventHandler.Handle(@event, _requestHandlerResolver);
        }

        /// <summary>
        /// Sends a stream of event concurrently to the bus
        /// </summary>
        /// <param name="events" cref="IEnumerable{Event}">Event stream</param>
        /// <returns>Completed Task</returns>
        public async Task Send(IEnumerable<Event> events)
        {
            if (events == null)
                return;

            IList<Task> sendEvents = new List<Task>();
            foreach (var @event in events)
            {
                sendEvents.Add(Send(@event));
            }
            await Task.WhenAll(sendEvents);
        }
    }
}
