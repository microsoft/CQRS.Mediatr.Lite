using System;
using System.Linq;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext
{
    public class OrderAggregateRoot
    {
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Id));
                if (!Guid.TryParse(value, out _))
                    throw new ArgumentException("Order ID must be a guid", nameof(Id));
                _id = value;
            }
        }

        public OrderStatus Status { get; private set; }
        public Customer Customer { get; private set; }

        public DateTime? CreationDate { get; private set; }
        public DateTime? ShippedDate { get; private set; }
        public DateTime? DeliveryDate { get; private set; }

        public string ShippingAddress { get; private set; }

        public IList<Product> Products { get; private set; }

        public IList<Event> UncommittedEvents { get; private set; } = new List<Event>();

        public double Cost
        {
            get
            {
                if (Products == null || !Products.Any())
                    return 0;
                double cost = 0;
                foreach (Product product in Products)
                {
                    cost += product.Price;
                }
                return cost;   
            }
        }

        public OrderAggregateRoot()
        {
            Products = new List<Product>();
        }

        public OrderAggregateRoot(
            string orderId,
            string orderStatus,
            string customerId,
            string customerName,
            string billingAddress,
            string createdOn,
            string shippedOn,
            string deliveredOn,
            string shippingAddress) : this()
        {
            Id = orderId;
            Status = new OrderStatus(orderStatus);
            Customer = new Customer(customerId, customerName, billingAddress);
            CreationDate = string.IsNullOrWhiteSpace(createdOn) ? null : DateTime.Parse(createdOn);
            ShippedDate = string.IsNullOrWhiteSpace(shippedOn) ? null : DateTime.Parse(shippedOn);
            DeliveryDate = string.IsNullOrWhiteSpace(deliveredOn) ? null : DateTime.Parse(deliveredOn);
            ShippingAddress = shippingAddress;
        }

        public void CreateOrder(CustomerDto customer, IEnumerable<ProductDto> products)
        {
            Id = Guid.NewGuid().ToString();
            Customer = new Customer(customer.CustomerId, customer.CustomerName, customer.CustomerBillingAddress);
            if (products != null && products.Any())
            {
                foreach(ProductDto product in products)
                {
                    Products.Add(new Product(product.ProductId, product.ProductName, product.ProductDescription));
                }
            }
            Status = new OrderStatus("Placed");
            CreationDate = DateTime.UtcNow.Date;
            UncommittedEvents.Add(new OrderCreated(this));
        }

        public void ShipOrder()
        {
            if (Status.Status != "Placed")
                throw new Exception("Only placed orders can be shipped");
            Status = new OrderStatus("Shipped");
            ShippedDate = DateTime.UtcNow;
            UncommittedEvents.Add(new OrderCreated(this));
        }

        public void DeliverOrder()
        {
            if (Status.Status != "Shipped")
                throw new Exception("Only shipped orders can be delivered");
            Status = new OrderStatus("Delivered");
            DeliveryDate = DateTime.UtcNow;
            UncommittedEvents.Add(new OrderDelivered(this));
        }
    }
}
