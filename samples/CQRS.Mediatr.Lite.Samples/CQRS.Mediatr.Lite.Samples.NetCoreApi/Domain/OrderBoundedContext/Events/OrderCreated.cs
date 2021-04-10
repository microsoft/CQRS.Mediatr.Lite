namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext
{
    public class OrderCreated : Event
    {
        public override string Id { get; set; }
        public override string DisplayName => nameof(OrderCreated);
        public string CustomerName { get; set; }
        public double Cost { get; set; }

        public OrderCreated(OrderAggregateRoot order)
        {
            Id = order.Id;
            CustomerName = order.Customer?.Name;
            Cost = order.Cost;
        }
    }
}
