namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext
{
    public class OrderDelivered: Event
    {
        public override string Id { get; set; }
        public override string DisplayName => nameof(OrderDelivered);


        public OrderDelivered(OrderAggregateRoot order)
        {
            Id = order.Id;
        }
    }
}
