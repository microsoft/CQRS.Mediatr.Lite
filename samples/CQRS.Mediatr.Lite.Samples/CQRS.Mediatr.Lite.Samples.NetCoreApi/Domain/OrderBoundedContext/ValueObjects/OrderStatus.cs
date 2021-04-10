using System;
using System.Collections.Generic;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext
{
    public class OrderStatus : ValueObject
    {
        protected List<string> AllowedStatus = new List<string>
        {
            "Draft",
            "Placed",
            "Shipped",
            "Delivered"
        };

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Status));
                if (!AllowedStatus.Contains(value))
                    throw new Exception("Invalid order status");
                _status = value;
            }
        }

        public OrderStatus(string status)
        {
            Status = status;
        }
    }
}
