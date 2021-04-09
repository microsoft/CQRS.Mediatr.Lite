using System.Threading.Tasks;
using System.Collections.Generic;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Bus for sending events
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Sends an event to the bus
        /// </summary>
        /// <param name="event" cref="Event">Event being published</param>
        /// <returns>Completed Task</returns>
        Task Send(Event @event);

        /// <summary>
        /// Sends a stream of event to the bus
        /// </summary>
        /// <param name="events" cref="IEnumerable{Event}">Event stream</param>
        /// <returns>Completed Task</returns>
        Task Send(IEnumerable<Event> events);
    }
}
