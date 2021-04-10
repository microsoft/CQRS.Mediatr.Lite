using System;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Commands
{
    public class ShipOrderCommand : Command<IdCommandResult>
    {
        public override string DisplayName => nameof(ShipOrderCommand);

        public override string Id { get; }

        public string OrderId { get; set; }

        public ShipOrderCommand(string orderId)
        {
            Id = Guid.NewGuid().ToString();
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
