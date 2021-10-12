using System.Threading.Tasks;
using System.Collections.Generic;

namespace CQRS.Mediatr.Lite.SDK.Domain
{
    /// <summary>
    /// Base aggregate root for implementing DDD
    /// </summary>
    public abstract class AggregateRoot
    {
        protected string _id;
        public string Id => _id;

        private readonly IList<Event> _uncommittedChanges;

        public AggregateRoot(string id)
        {
            _id = id;
            _uncommittedChanges = new List<Event>();
        }

        public void ApplyChange(Event @event)
        {
            _uncommittedChanges.Add(@event);
        }

        public IEnumerable<Event> GetUncommittedChanges() => _uncommittedChanges;

        public void Commit()
        {
            _uncommittedChanges.Clear();
        }

        public async Task Commit(IEventBus eventBus)
        {
            await eventBus.Send(_uncommittedChanges);
            Commit();
        }
    }
}
