namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base handler for Events
    /// </summary>
    /// <typeparam name="TEvent">Type of the event</typeparam>
    public abstract class EventHandler<TEvent>
        : RequestHandler<TEvent, VoidResult>
        where TEvent: Event
    { }
}
