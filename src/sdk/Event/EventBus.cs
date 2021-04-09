using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using System.Collections.Generic;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Bus for sending events
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, object> _eventHandlerWrappers;
        private readonly IRequestHandlerResolver _requestHandlerResolver;

        /// <summary>
        /// Constructs the bus
        /// </summary>
        /// <param name="requestHandlerResolver">Resolved event handlers</param>
        public EventBus(IRequestHandlerResolver requestHandlerResolver)
        {
            _requestHandlerResolver = requestHandlerResolver;
            _eventHandlerWrappers = new Dictionary<Type, object>();
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
        /// Sends a stream of event to the bus
        /// </summary>
        /// <param name="events" cref="IEnumerable{Event}">Event stream</param>
        /// <returns>Completed Task</returns>
        public async Task Send(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                await Send(@event);
            }
        }
    }
}
