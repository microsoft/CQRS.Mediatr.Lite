namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext
{
    public class OrderShipped: Event
    {
        public override string Id { get; set; }
        public override string DisplayName => nameof(OrderShipped);

        public OrderShipped(OrderAggregateRoot order)
        {
            Id = order.Id;
        }
    }
}
