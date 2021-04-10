using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Queries
{
    public class GetOrderQuery : Query<OrderDto>
    {
        public override string DisplayName => nameof(GetOrderQuery);

        public override string Id { get; }

        public string OrderId { get; set; }

        public GetOrderQuery(string orderId)
        {
            OrderId = orderId;
        }

        public override bool Validate(out string ValidationErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(OrderId))
            {
                ValidationErrorMessage = "Order ID cannot be null";
                return false;
            }
            ValidationErrorMessage = null;
            return true;
        }
    }
}
