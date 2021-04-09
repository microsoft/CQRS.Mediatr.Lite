using System.Linq;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CQRS.Mediatr.Lite.Tests")]
namespace CQRS.Mediatr.Lite.Internal
{
    internal interface IEventHandlerWrapper
    {
        Task Handle(Event @event, IRequestHandlerResolver requestHandlerResolver);
    }

    internal class EventHandlerWrapper<TEvent>: IEventHandlerWrapper where TEvent: Event
    {
        public async Task Handle(Event @event, IRequestHandlerResolver requestHandlerResolver)
        {
            try
            {
                var eventHandlers = requestHandlerResolver.ResolveAll<EventHandler<TEvent>>();
                if (eventHandlers != null && eventHandlers.Count() > 0)
                {
                    foreach (var eventHandler in eventHandlers)
                    {
                        await eventHandler.Handle((TEvent)@event);
                    }
                }
            }
            catch (HandlerNotFoundException)
            {
                //Events are allowed to have no handlers
                return;
            }
        }
    }
}
